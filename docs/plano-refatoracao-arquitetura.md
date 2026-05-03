# Plano de Refatoração Arquitetural — CMSX (Multiplai)

> **Status:** Aguardando aprovação. Nenhuma implementação antes do aceite.
> **Data:** 2026-05-03

---

## 1. Contexto e Motivação

O CMSX foi originalmente escrito em WebForms + ADO.NET para SQL Server. Ao longo das refatorações, foram adicionados ASP.NET Core 8, EF Core 8 e suporte a PostgreSQL — mas sem remover os atalhos criados no processo. O resultado é uma arquitetura híbrida e inconsistente que:

- Expõe `CmsxDbContext` (EF Core) diretamente em controllers
- Tem LINQ to Entities vazando para fora da camada de dados
- Quebra o padrão Strategy/Factory para múltiplos bancos
- Possui dívida técnica em conversões de tipo (`.ToString()` em queries) que só aparece no PostgreSQL
- Não trata adequadamente dados sensíveis em todas as camadas

---

## 2. Regras Arquiteturais — Imutáveis

Estas regras se aplicam a **todo agente, sessão e implementação** neste projeto:

### Proibições absolutas
- **PROIBIDO** usar `Microsoft.EntityFrameworkCore` ou qualquer referência a `DbContext` fora das camadas `CMSXData` e `CMSXRepo`
- **PROIBIDO** injetar `CmsxDbContext` em controllers, services ou qualquer camada fora de Infrastructure/Data
- **PROIBIDO** expor `IQueryable<T>` fora do repositório (LINQ to Entities é vazamento de estrutura)
- **PROIBIDO** tomar atalhos para "fazer funcionar" em detrimento da arquitetura

### Obrigações
- **OBRIGATÓRIO** toda persistência via interfaces definidas em `ICMSX`
- **OBRIGATÓRIO** se a interface não existir: criar a interface em `ICMSX` primeiro, depois a implementação em `CMSXRepo`
- **OBRIGATÓRIO** encapsular queries em métodos semânticos no repositório: `GetByInternalCode(string code)`, nunca expor predicados para fora
- **OBRIGATÓRIO** a Factory/Repository garante conversões de tipo na fronteira — a camada de serviço pede um objeto, não sabe se o SQL usa `CAST`, `CONVERT` ou `::text`
- **OBRIGATÓRIO** considerar segurança de dados sensíveis antes de qualquer implementação ou ajuste

### Princípios que guiam cada decisão
- **Inversão de Dependência**: controllers dependem de `ICMSX`, nunca de implementações concretas
- **Encapsulamento de Query**: queries ficam no repositório, não nos controllers
- **Tratamento de Tipos na Fronteira**: conversões acontecem no repositório/factory, não no controller
- **Multi-banco transparente**: o código de negócio não sabe qual banco está sendo usado

---

## 3. Mapeamento da Arquitetura Atual

```
Presentation     →  CMSAPI/Controllers, CMSUI (Angular), PublicUI
Application      →  (ausente — lógica misturada nos controllers)
Domain/Core      →  ICMSX (interfaces)
Infrastructure   →  CMSXRepo (repositórios EF Core)
                    CMSXDAO (DAL ADO.NET — legado)
Data             →  CMSXData (Models, DbContext, Migrations)
```

### Estado atual dos controllers (23 no CMSAPI)
| Controller | Injeta | Situação |
|---|---|---|
| OrcamentosController | IOrcamentoRepositorio, IOrcamentoCompostoRepositorio | ✅ Correto |
| LojaController | ILojaRepositorio | ✅ Correto |
| ProdutosController | CmsxDbContext + IProdutoMaoDeObraRepositorio | ❌ Híbrido |
| PageBuilderController | CmsxDbContext + IAgentIAFactory | ❌ Híbrido |
| UsuariosController | CmsxDbContext | ❌ Direto |
| AuthController | CmsxDbContext | ❌ Direto |
| AplicacaosController | CmsxDbContext | ❌ Direto |
| AreasController | CmsxDbContext | ❌ Direto |
| ConteudosController | CmsxDbContext | ❌ Direto |
| SiteController | CmsxDbContext | ❌ Direto |
| DashboardController | CmsxDbContext | ❌ Direto |
| CategoriasController | CmsxDbContext | ❌ Direto |
| FaqController | CmsxDbContext | ❌ Direto |
| FormulariosController | CmsxDbContext | ❌ Direto |
| GruposController | CmsxDbContext | ❌ Direto |
| LayoutTemplatesController | CmsxDbContext | ❌ Direto |
| ModulosController | CmsxDbContext | ❌ Direto |
| PedidosController | CmsxDbContext | ❌ Direto |
| PublicTokensController | CmsxDbContext | ❌ Direto |
| RelmodulousuariosController | CmsxDbContext | ❌ Direto |
| RelusuarioaplicacaoController | CmsxDbContext | ❌ Direto |
| MarketplaceController | CmsxDbContext + MarketHubHttpService | ❌ Híbrido |

---

## 4. Dívida Técnica Mapeada

### 4.1 Vazamento de EF Core para controllers
- 15+ controllers com `CmsxDbContext` injetado diretamente
- LINQ to Entities rodando nos controllers
- Queries sem encapsulamento semântico

### 4.2 Incompatibilidade multi-banco (SQL Server × PostgreSQL)
- `.ToString()` em expressões LINQ (já corrigido pontualmente, mas o padrão deve ser eliminado via repositório)
- `DateTime.Now` vs `DateTime.UtcNow` — o repositório deve garantir `UtcNow` em todas as escritas
- Tipos inconsistentes: IDs armazenados como `string` mas tratados como `Guid` em alguns pontos

### 4.3 DAL legado (CMSXDAO) convivendo com EF Core
- ADO.NET puro com `DataTable`/`DataReader` em CMSXDAO
- `ProdutoDAL.cs` instancia `new CmsxDbContext()` sem DI — violação crítica
- Strategy/Factory de bancos implementado em CMSXDAO mas não usado pelos repositórios novos

### 4.4 Repositórios stub
- ~18 repositórios em CMSXRepo lançam `NotImplementedException()`
- Interfaces em ICMSX sem implementação funcional

### 4.5 Dados sensíveis expostos
- Credenciais de e-mail, tokens de pagamento (PagSeguro), credenciais de marketplace trafegam por controllers sem validação de escopo/tenant
- Ausência de sanitização consistente em campos de entrada antes da persistência
- Logs de erro podem expor dados internos (stack traces com dados de query)

---

## 5. Plano de Execução — Fases

### Fase 0 — Preparação e contrato (sem código)
- [ ] Definir e documentar o contrato de cada interface que será criada/completada em ICMSX
- [ ] Mapear campos sensíveis por entidade (quais devem ser mascarados em logs, quais exigem validação de tenant)
- [ ] Definir convenção de nomenclatura para métodos de repositório (semânticos, não genéricos)
- [ ] Validar este plano com o responsável antes de avançar

### Fase 1 — Interfaces e contratos (ICMSX)
Para cada controller com DbContext direto, criar ou completar a interface correspondente em ICMSX com métodos semânticos. Nenhuma implementação ainda.

Ordem de prioridade (por uso e risco):
1. `IAplicacaoRepositorio` — usado em autenticação e resolução de tenant
2. `IUsuarioRepositorio` — dados sensíveis (senha, acesso)
3. `IAuthRepositorio` — login, JWT
4. `IAreasRepositorio` — Page Builder depende
5. `IConteudoRepositorio`
6. `ISiteRepositorio`
7. `IDashboardRepositorio`
8. Demais (Categorias, FAQ, Formularios, Grupos, LayoutTemplates, Modulos, Pedidos, PublicTokens, Relações)

### Fase 2 — Implementações (CMSXRepo)
Para cada interface da Fase 1, implementar em CMSXRepo:
- Herda `BaseRepositorio` (acesso via `_db` — CmsxDbContext isolado aqui)
- Métodos semânticos: nenhum retorno de `IQueryable<T>`
- Conversões de tipo feitas internamente: nenhum `.ToString()` em predicados EF
- `DateTime.UtcNow` garantido internamente para campos de auditoria
- Validação de tenant dentro do repositório (nunca confiar no controller para filtrar)
- Dados sensíveis: repositórios de usuário/auth nunca retornam senha, mesmo em objetos internos

### Fase 3 — Migração dos controllers
Substituir `CmsxDbContext` por `IXRepositorio` em cada controller:
- Um controller por vez
- Sem alterar comportamento externo (mesmos endpoints, mesmos DTOs)
- Controllers não fazem mais nenhuma operação de dados — apenas orquestram

### Fase 4 — Deprecação do CMSXDAO
- Avaliar quais funcionalidades do CMSXDAO ainda são usadas em produção
- Migrar o que for necessário para CMSXRepo
- Remover CMSXDAO do build após validação

### Fase 5 — Validação multi-banco
- Rodar suite de testes em SQL Server e PostgreSQL
- Garantir que o Strategy de banco (DatabaseProvider no appsettings) funciona de ponta a ponta pela nova estrutura

---

## 6. Regras para Agentes

Todo agente que trabalhar neste projeto deve:

1. **Ler este documento antes de qualquer implementação**
2. **Nunca criar atalho** para fazer funcionar mais rápido em detrimento da arquitetura
3. **Se a interface não existir**, criar em ICMSX primeiro e apresentar para aprovação antes da implementação
4. **Nunca injetar CmsxDbContext** fora de CMSXRepo/CMSXData — se precisar de dados, criar o método no repositório
5. **Nunca expor IQueryable** — retornar `IEnumerable<T>`, `T?`, ou DTOs específicos
6. **Tratar dados sensíveis**: usuários, senhas, tokens, credenciais — nunca logar, nunca retornar em plain text desnecessariamente
7. **Validar escopo de tenant** dentro do repositório, não no controller
8. **Reportar como impeditivo** se encontrar ambiguidade sobre qual interface criar ou qual comportamento preservar

---

## 7. O que NÃO está no escopo deste plano

- Mudança de endpoints ou contratos da API pública
- Migração de CMSXDAO para EF Core além do necessário para a Fase 3
- Refatoração do Angular (CMSUI/PublicUI)
- Alterações no schema do banco (novas migrations)
- Reescrita do sistema de autenticação

---

## Aprovação

- [ ] Aprovado pelo responsável do projeto
- Data de aprovação: ___________
- Observações: ___________
