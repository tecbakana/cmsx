-- --------------------------------------------------
-- CMSX Database - SQL Server Express
-- Script corrigido: schemas, banco, tabela faltante
-- --------------------------------------------------

-- 1. Criar o banco se não existir
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'cmsxDB')
BEGIN
    CREATE DATABASE [cmsxDB];
END
GO

USE [cmsxDB];
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing tables (schema corrigido para dbo)
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[aplicacao]', 'U') IS NOT NULL DROP TABLE [dbo].[aplicacao];
GO
IF OBJECT_ID(N'[dbo].[areas]', 'U') IS NOT NULL DROP TABLE [dbo].[areas];
GO
IF OBJECT_ID(N'[dbo].[arquivo]', 'U') IS NOT NULL DROP TABLE [dbo].[arquivo];
GO
IF OBJECT_ID(N'[dbo].[atributo]', 'U') IS NOT NULL DROP TABLE [dbo].[atributo];
GO
IF OBJECT_ID(N'[dbo].[categoria]', 'U') IS NOT NULL DROP TABLE [dbo].[categoria];
GO
IF OBJECT_ID(N'[dbo].[conteudo]', 'U') IS NOT NULL DROP TABLE [dbo].[conteudo];
GO
IF OBJECT_ID(N'[dbo].[conteudovalor]', 'U') IS NOT NULL DROP TABLE [dbo].[conteudovalor];
GO
IF OBJECT_ID(N'[dbo].[dict_socialmedia]', 'U') IS NOT NULL DROP TABLE [dbo].[dict_socialmedia];
GO
IF OBJECT_ID(N'[dbo].[dict_templates]', 'U') IS NOT NULL DROP TABLE [dbo].[dict_templates];
GO
IF OBJECT_ID(N'[dbo].[dictareas]', 'U') IS NOT NULL DROP TABLE [dbo].[dictareas];
GO
IF OBJECT_ID(N'[dbo].[formulario]', 'U') IS NOT NULL DROP TABLE [dbo].[formulario];
GO
IF OBJECT_ID(N'[dbo].[formularionew]', 'U') IS NOT NULL DROP TABLE [dbo].[formularionew];
GO
IF OBJECT_ID(N'[dbo].[imagem]', 'U') IS NOT NULL DROP TABLE [dbo].[imagem];
GO
IF OBJECT_ID(N'[dbo].[modulo]', 'U') IS NOT NULL DROP TABLE [dbo].[modulo];
GO
IF OBJECT_ID(N'[dbo].[opcao]', 'U') IS NOT NULL DROP TABLE [dbo].[opcao];
GO
IF OBJECT_ID(N'[dbo].[produto]', 'U') IS NOT NULL DROP TABLE [dbo].[produto];
GO
IF OBJECT_ID(N'[dbo].[refatributoxopcao]', 'U') IS NOT NULL DROP TABLE [dbo].[refatributoxopcao];
GO
IF OBJECT_ID(N'[dbo].[relatributoxproduto]', 'U') IS NOT NULL DROP TABLE [dbo].[relatributoxproduto];
GO
IF OBJECT_ID(N'[dbo].[relimagemconteudo]', 'U') IS NOT NULL DROP TABLE [dbo].[relimagemconteudo];
GO
IF OBJECT_ID(N'[dbo].[relmoduloaplicacao]', 'U') IS NOT NULL DROP TABLE [dbo].[relmoduloaplicacao];
GO
IF OBJECT_ID(N'[dbo].[relmoduloconfaplicacao]', 'U') IS NOT NULL DROP TABLE [dbo].[relmoduloconfaplicacao];
GO
IF OBJECT_ID(N'[dbo].[relmodulousuario]', 'U') IS NOT NULL DROP TABLE [dbo].[relmodulousuario];
GO
IF OBJECT_ID(N'[dbo].[relusuarioaplicacao]', 'U') IS NOT NULL DROP TABLE [dbo].[relusuarioaplicacao];
GO
IF OBJECT_ID(N'[dbo].[socialmedia]', 'U') IS NOT NULL DROP TABLE [dbo].[socialmedia];
GO
IF OBJECT_ID(N'[dbo].[templates]', 'U') IS NOT NULL DROP TABLE [dbo].[templates];
GO
IF OBJECT_ID(N'[dbo].[unidades]', 'U') IS NOT NULL DROP TABLE [dbo].[unidades];
GO
IF OBJECT_ID(N'[dbo].[usuario]', 'U') IS NOT NULL DROP TABLE [dbo].[usuario];
GO
IF OBJECT_ID(N'[dbo].[cambio]', 'U') IS NOT NULL DROP TABLE [dbo].[cambio];
GO
IF OBJECT_ID(N'[dbo].[ciaaerea]', 'U') IS NOT NULL DROP TABLE [dbo].[ciaaerea];
GO
IF OBJECT_ID(N'[dbo].[infofoto]', 'U') IS NOT NULL DROP TABLE [dbo].[infofoto];
GO
IF OBJECT_ID(N'[dbo].[informativo]', 'U') IS NOT NULL DROP TABLE [dbo].[informativo];
GO
IF OBJECT_ID(N'[dbo].[moduloconf]', 'U') IS NOT NULL DROP TABLE [dbo].[moduloconf];
GO
IF OBJECT_ID(N'[dbo].[moeda]', 'U') IS NOT NULL DROP TABLE [dbo].[moeda];
GO
IF OBJECT_ID(N'[dbo].[newsletter]', 'U') IS NOT NULL DROP TABLE [dbo].[newsletter];
GO
IF OBJECT_ID(N'[dbo].[tipocotacao]', 'U') IS NOT NULL DROP TABLE [dbo].[tipocotacao];
GO
IF OBJECT_ID(N'[dbo].[tipoenvio]', 'U') IS NOT NULL DROP TABLE [dbo].[tipoenvio];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'arquivo'
CREATE TABLE [dbo].[arquivo] (
    [ArquivoId] varchar(64)  NOT NULL,
    [AreaId] varchar(64)  NULL,
    [ConteudoId] varchar(64)  NULL,
    [Nome] varchar(64)  NULL,
    [TipoId] varchar(64)  NULL
);
GO

-- Creating table 'atributo'
CREATE TABLE [dbo].[atributo] (
    [AtributoId] uniqueidentifier  NOT NULL,
    [Nome] varchar(45)  NOT NULL,
    [Descricao] varchar(45)  NOT NULL,
    [ProdutoId] varchar(64)  NULL
);
GO

-- Creating table 'conteudo'
CREATE TABLE [dbo].[conteudo] (
    [ConteudoId] varchar(64)  NOT NULL,
    [AreaId] varchar(64)  NULL,
    [Autor] varchar(80)  NULL,
    [Titulo] varchar(80)  NULL,
    [Texto] varchar(max)  NULL,
    [DataInclusao] datetime  NULL,
    [DataFinal] datetime  NULL,
    [CategoriaId] varchar(64)  NULL
);
GO

-- Creating table 'conteudovalor'
CREATE TABLE [dbo].[conteudovalor] (
    [ConteudoId] varchar(64)  NOT NULL,
    [UnidadeId] uniqueidentifier  NULL,
    [Valor] decimal(10,2)  NULL
);
GO

-- Creating table 'dict_templates'
CREATE TABLE [dbo].[dict_templates] (
    [idTemplate] varchar(100)  NOT NULL,
    [Nome] varchar(255)  NULL,
    [Descricao] varchar(255)  NULL,
    [viewRelacionada] varchar(45)  NULL
);
GO

-- Creating table 'dictareas'
CREATE TABLE [dbo].[dictareas] (
    [id] int  NOT NULL,
    [nome] varchar(12)  NULL,
    [tipo] int  NOT NULL
);
GO

-- Creating table 'formulario'
CREATE TABLE [dbo].[formulario] (
    [formularioid] varchar(64)  NOT NULL,
    [nome] varchar(255)  NOT NULL,
    [valor] varchar(8000)  NULL,
    [ativo] bit  NULL,
    [datainclusao] datetime  NULL,
    [areaid] varchar(64)  NULL
);
GO

-- Creating table 'formularionew'
CREATE TABLE [dbo].[formularionew] (
    [IdForm] int IDENTITY(1,1) NOT NULL,
    [Nome] varchar(100)  NULL,
    [Tipo] int  NULL,
    [Email] varchar(155)  NULL,
    [Telefone] varchar(15)  NULL,
    [Texto] varchar(255)  NULL,
    [Ativo] int  NULL
);
GO

-- Creating table 'imagem'
CREATE TABLE [dbo].[imagem] (
    [ImagemId] varchar(64)  NOT NULL,
    [Url] varchar(300)  NULL,
    [Largura] int  NULL,
    [Altura] int  NULL,
    [AreaId] varchar(64)  NULL,
    [ConteudoId] varchar(64)  NULL,
    [Descricao] varchar(255)  NULL,
    [ParentId] varchar(64)  NULL,
    [TipoId] varchar(64)  NULL
);
GO

-- Creating table 'modulo'
CREATE TABLE [dbo].[modulo] (
    [ModuloId] varchar(64)  NOT NULL,
    [Nome] varchar(400)  NULL,
    [Url] varchar(400)  NULL,
    [Posicao] int  NULL
);
GO

-- Creating table 'opcao'
CREATE TABLE [dbo].[opcao] (
    [OpcaoId] varchar(64)  NOT NULL,
    [AtributoId] uniqueidentifier  NOT NULL,
    [Qtd] int  NOT NULL,
    [Nome] varchar(500)  NULL,
    [Descricao] varchar(500)  NULL,
    [Estoque] int  NULL
);
GO

-- Creating table 'produto'
CREATE TABLE [dbo].[produto] (
    [ProdutoId] varchar(64)  NOT NULL,
    [Nome] varchar(130)  NULL,
    [Descricao] varchar(max)  NULL,
    [Valor] decimal(18,2)  NULL,
    [Tipo] int  NULL,
    [Destaque] int  NULL,
    [DataInicio] datetime  NULL,
    [DataFinal] datetime  NULL,
    [CategoriaId] varchar(64)  NULL,
    [AplicacaoId] varchar(64)  NULL,
    [sku] varchar(45)  NOT NULL,
    [PagSeguroKey] varchar(800)  NULL,
    [DetalheTecnico] varchar(max)  NULL,
    [DescricaCurta] varchar(150)  NULL,
    [produtocol] varchar(45)  NULL
);
GO

-- Creating table 'refatributoxopcao'
CREATE TABLE [dbo].[refatributoxopcao] (
    [relacaoid] uniqueidentifier  NOT NULL,
    [atributoid] uniqueidentifier  NOT NULL,
    [opcaoid] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'relatributoxproduto'
CREATE TABLE [dbo].[relatributoxproduto] (
    [Relacaoid] uniqueidentifier  NOT NULL,
    [Atributoid] uniqueidentifier  NOT NULL,
    [ProdutoId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'unidades'
CREATE TABLE [dbo].[unidades] (
    [UnidadeId] uniqueidentifier  NOT NULL,
    [Nome] varchar(45)  NULL,
    [Sigla] varchar(45)  NULL
);
GO

-- Creating table 'usuario'
CREATE TABLE [dbo].[usuario] (
    [UserId] varchar(64)  NOT NULL,
    [Nome] varchar(300)  NOT NULL,
    [Sobrenome] varchar(300)  NOT NULL,
    [Apelido] varchar(6)  NULL,
    [Senha] varchar(12)  NULL,
    [Ativo] tinyint  NULL,
    [DataInclusao] datetime  NULL
);
GO

-- Creating table 'cambio'
CREATE TABLE [dbo].[cambio] (
    [CambioGroupId] varchar(64)  NOT NULL,
    [DataCotacao] datetime  NULL,
    [MoedasXml] varchar(1000)  NULL,
    [Tipo] tinyint  NULL
);
GO

-- Creating table 'categoria'
CREATE TABLE [dbo].[categoria] (
    [CategoriaId] varchar(64)  NOT NULL,
    [Nome] varchar(200)  NULL,
    [Descricao] varchar(1000)  NULL,
    [TipoCategoria] int  NULL,
    [CategoriaIdPai] varchar(64)  NULL,
    [AplicacaoId] varchar(64)  NULL
);
GO

-- Creating table 'ciaaerea'
CREATE TABLE [dbo].[ciaaerea] (
    [CiaAereaId] varchar(64)  NOT NULL,
    [Descricao] varchar(4000)  NULL,
    [Logotipo] varchar(300)  NULL,
    [Descricao_Longa] varchar(max)  NULL,
    [Ativo] tinyint  NULL,
    [TipoNac] tinyint  NULL,
    [TipoInt] tinyint  NULL,
    [webticket_str] varchar(450)  NULL
);
GO

-- Creating table 'infofoto'
CREATE TABLE [dbo].[infofoto] (
    [FotoId] varchar(64)  NOT NULL,
    [FotoUrl] varchar(300)  NULL,
    [Descricao] varchar(1000)  NULL,
    [CategoriaId] varchar(64)  NOT NULL
);
GO

-- Creating table 'informativo'
CREATE TABLE [dbo].[informativo] (
    [InfoId] varchar(64)  NOT NULL,
    [Titulo] varchar(300)  NULL,
    [Data] datetime  NULL,
    [Texto] varchar(max)  NULL,
    [Foto] varchar(64)  NULL,
    [Ativo] tinyint  NULL,
    [TipoEnvio] varchar(64)  NULL
);
GO

-- Creating table 'moduloconf'
CREATE TABLE [dbo].[moduloconf] (
    [ModuloConfId] varchar(64)  NOT NULL,
    [Descricao] varchar(800)  NULL,
    [Nome] varchar(200)  NULL
);
GO

-- Creating table 'moeda'
CREATE TABLE [dbo].[moeda] (
    [MoedaId] varchar(64)  NOT NULL,
    [Nome] varchar(80)  NOT NULL,
    [Sigla] char(34)  NULL
);
GO

-- Creating table 'newsletter'
CREATE TABLE [dbo].[newsletter] (
    [NewsId] varchar(64)  NOT NULL,
    [Titulo] varchar(3000)  NULL,
    [Autor] varchar(1000)  NULL,
    [Data] datetime  NULL,
    [Frente] tinyint  NULL,
    [Texto] varchar(max)  NULL,
    [Foto] varchar(4000)  NULL,
    [CategoriaId] varchar(64)  NULL,
    [Ativo] tinyint  NULL
);
GO

-- Creating table 'relmoduloaplicacao'
CREATE TABLE [dbo].[relmoduloaplicacao] (
    [RelacaoId] varchar(64)  NOT NULL,
    [AplicacaoId] varchar(64)  NOT NULL,
    [ModuloId] varchar(64)  NOT NULL
);
GO

-- Creating table 'relmoduloconfaplicacao'
CREATE TABLE [dbo].[relmoduloconfaplicacao] (
    [RelacaoId] varchar(64)  NOT NULL,
    [ModuloConfId] varchar(64)  NOT NULL,
    [AplicacaoId] varchar(64)  NOT NULL,
    [DataInclusao] datetime  NOT NULL,
    [DataFinalizacao] datetime  NOT NULL
);
GO

-- Creating table 'relmodulousuario' (faltava nos dois scripts originais)
CREATE TABLE [dbo].[relmodulousuario] (
    [RelacaoId] varchar(64)  NOT NULL,
    [ModuloId] varchar(64)  NOT NULL,
    [UsuarioId] varchar(64)  NOT NULL
);
GO

-- Creating table 'relusuarioaplicacao'
CREATE TABLE [dbo].[relusuarioaplicacao] (
    [RelacaoId] varchar(64)  NOT NULL,
    [AplicacaoId] varchar(64)  NOT NULL,
    [UsuarioId] varchar(64)  NOT NULL
);
GO

-- Creating table 'tipocotacao'
CREATE TABLE [dbo].[tipocotacao] (
    [TipoCotacaoId] varchar(64)  NOT NULL,
    [Nome] varchar(200)  NULL,
    [Descricao] varchar(200)  NULL
);
GO

-- Creating table 'tipoenvio'
CREATE TABLE [dbo].[tipoenvio] (
    [TipoEnvioId] varchar(64)  NOT NULL,
    [TipoEnvioDesc] varchar(300)  NULL
);
GO

-- Creating table 'socialmedia'
CREATE TABLE [dbo].[socialmedia] (
    [SocialMediaId] varchar(45)  NOT NULL,
    [AplicacaoId] varchar(45)  NULL,
    [SocialMediaTypeId] int  NULL,
    [SocialMediaLink] varchar(255)  NULL
);
GO

-- Creating table 'dict_socialmedia'
CREATE TABLE [dbo].[dict_socialmedia] (
    [SocialMediaId] int  NOT NULL,
    [SocialMediaName] varchar(45)  NULL,
    [SocialMediaUrl] varchar(45)  NULL
);
GO

-- Creating table 'aplicacao'
CREATE TABLE [dbo].[aplicacao] (
    [AplicacaoId] varchar(64)  NOT NULL,
    [Nome] varchar(400)  NULL,
    [Url] varchar(20)  NULL,
    [DataInicio] datetime  NULL,
    [DataFinal] datetime  NULL,
    [IdUsuarioInicio] varchar(36)  NULL,
    [IdUsuarioFim] varchar(36)  NULL,
    [PagSeguroToken] varchar(120)  NULL,
    [LayoutChoose] varchar(150)  NULL,
    [Posicao] int  NULL,
    [mailUser] varchar(150)  NULL,
    [mailPassword] varchar(45)  NULL,
    [mailServer] varchar(80)  NULL,
    [mailPort] int  NULL,
    [isSecure] tinyint  NULL,
    [pageFacebook] varchar(255)  NULL,
    [pageLinkedin] varchar(255)  NULL,
    [pageInstagram] varchar(255)  NULL,
    [pageTwitter] varchar(255)  NULL,
    [pagePinterest] varchar(255)  NULL,
    [pageFlicker] varchar(255)  NULL,
    [logotipo] varbinary(max)  NULL,
    [googleAdSense] varchar(500)  NULL,
    [isActive] bit  NULL,
    [header] varchar(245)  NULL
);
GO

-- Creating table 'areas'
CREATE TABLE [dbo].[areas] (
    [AreaId] varchar(64)  NOT NULL,
    [AplicacaoId] varchar(64)  NULL,
    [Nome] varchar(80)  NULL,
    [Url] varchar(300)  NULL,
    [Descricao] varchar(255)  NULL,
    [AreaIdPai] varchar(64)  NULL,
    [DataInicial] datetime  NULL,
    [DataFinal] datetime  NULL,
    [Imagem] tinyint  NULL,
    [MenuLateral] tinyint  NULL,
    [MenuSplash] tinyint  NULL,
    [MenuCentral] tinyint  NULL,
    [posicao] int  NULL,
    [MenuFixo] tinyint  NULL,
    [ListaSimples] tinyint  NULL,
    [ListaSplash] tinyint  NULL,
    [ListaBanner] tinyint  NULL,
    [TipoArea] int  NULL
);
GO

-- Creating table 'relimagemconteudo'
CREATE TABLE [dbo].[relimagemconteudo] (
    [relid] int IDENTITY(1,1) NOT NULL,
    [imagemid] varchar(45)  NULL,
    [parentid] varchar(45)  NULL
);
GO

-- Creating table 'templates'
CREATE TABLE [dbo].[templates] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Nome] varchar(155)  NULL,
    [Descricao] varchar(255)  NULL,
    [Url] varchar(80)  NULL,
    [Ativo] bit  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

ALTER TABLE [dbo].[arquivo]
ADD CONSTRAINT [PK_arquivo] PRIMARY KEY CLUSTERED ([ArquivoId] ASC);
GO

ALTER TABLE [dbo].[atributo]
ADD CONSTRAINT [PK_atributo] PRIMARY KEY CLUSTERED ([AtributoId] ASC);
GO

ALTER TABLE [dbo].[conteudo]
ADD CONSTRAINT [PK_conteudo] PRIMARY KEY CLUSTERED ([ConteudoId] ASC);
GO

ALTER TABLE [dbo].[conteudovalor]
ADD CONSTRAINT [PK_conteudovalor] PRIMARY KEY CLUSTERED ([ConteudoId] ASC);
GO

ALTER TABLE [dbo].[dict_templates]
ADD CONSTRAINT [PK_dict_templates] PRIMARY KEY CLUSTERED ([idTemplate] ASC);
GO

ALTER TABLE [dbo].[dictareas]
ADD CONSTRAINT [PK_dictareas] PRIMARY KEY CLUSTERED ([id] ASC);
GO

ALTER TABLE [dbo].[formulario]
ADD CONSTRAINT [PK_formulario] PRIMARY KEY CLUSTERED ([formularioid], [nome] ASC);
GO

ALTER TABLE [dbo].[formularionew]
ADD CONSTRAINT [PK_formularionew] PRIMARY KEY CLUSTERED ([IdForm] ASC);
GO

ALTER TABLE [dbo].[imagem]
ADD CONSTRAINT [PK_imagem] PRIMARY KEY CLUSTERED ([ImagemId] ASC);
GO

ALTER TABLE [dbo].[modulo]
ADD CONSTRAINT [PK_modulo] PRIMARY KEY CLUSTERED ([ModuloId] ASC);
GO

ALTER TABLE [dbo].[opcao]
ADD CONSTRAINT [PK_opcao] PRIMARY KEY CLUSTERED ([OpcaoId] ASC);
GO

ALTER TABLE [dbo].[produto]
ADD CONSTRAINT [PK_produto] PRIMARY KEY CLUSTERED ([ProdutoId], [sku] ASC);
GO

ALTER TABLE [dbo].[refatributoxopcao]
ADD CONSTRAINT [PK_refatributoxopcao] PRIMARY KEY CLUSTERED ([relacaoid] ASC);
GO

ALTER TABLE [dbo].[relatributoxproduto]
ADD CONSTRAINT [PK_relatributoxproduto] PRIMARY KEY CLUSTERED ([Relacaoid] ASC);
GO

ALTER TABLE [dbo].[unidades]
ADD CONSTRAINT [PK_unidades] PRIMARY KEY CLUSTERED ([UnidadeId] ASC);
GO

ALTER TABLE [dbo].[usuario]
ADD CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

ALTER TABLE [dbo].[cambio]
ADD CONSTRAINT [PK_cambio] PRIMARY KEY CLUSTERED ([CambioGroupId] ASC);
GO

ALTER TABLE [dbo].[categoria]
ADD CONSTRAINT [PK_categoria] PRIMARY KEY CLUSTERED ([CategoriaId] ASC);
GO

ALTER TABLE [dbo].[ciaaerea]
ADD CONSTRAINT [PK_ciaaerea] PRIMARY KEY CLUSTERED ([CiaAereaId] ASC);
GO

ALTER TABLE [dbo].[infofoto]
ADD CONSTRAINT [PK_infofoto] PRIMARY KEY CLUSTERED ([FotoId], [CategoriaId] ASC);
GO

ALTER TABLE [dbo].[informativo]
ADD CONSTRAINT [PK_informativo] PRIMARY KEY CLUSTERED ([InfoId] ASC);
GO

ALTER TABLE [dbo].[moduloconf]
ADD CONSTRAINT [PK_moduloconf] PRIMARY KEY CLUSTERED ([ModuloConfId] ASC);
GO

ALTER TABLE [dbo].[moeda]
ADD CONSTRAINT [PK_moeda] PRIMARY KEY CLUSTERED ([MoedaId], [Nome] ASC);
GO

ALTER TABLE [dbo].[newsletter]
ADD CONSTRAINT [PK_newsletter] PRIMARY KEY CLUSTERED ([NewsId] ASC);
GO

ALTER TABLE [dbo].[relmoduloaplicacao]
ADD CONSTRAINT [PK_relmoduloaplicacao] PRIMARY KEY CLUSTERED ([RelacaoId], [AplicacaoId], [ModuloId] ASC);
GO

ALTER TABLE [dbo].[relmoduloconfaplicacao]
ADD CONSTRAINT [PK_relmoduloconfaplicacao] PRIMARY KEY CLUSTERED ([RelacaoId], [ModuloConfId], [AplicacaoId], [DataInclusao], [DataFinalizacao] ASC);
GO

ALTER TABLE [dbo].[relmodulousuario]
ADD CONSTRAINT [PK_relmodulousuario] PRIMARY KEY CLUSTERED ([RelacaoId], [ModuloId], [UsuarioId] ASC);
GO

ALTER TABLE [dbo].[relusuarioaplicacao]
ADD CONSTRAINT [PK_relusuarioaplicacao] PRIMARY KEY CLUSTERED ([RelacaoId], [AplicacaoId], [UsuarioId] ASC);
GO

ALTER TABLE [dbo].[tipocotacao]
ADD CONSTRAINT [PK_tipocotacao] PRIMARY KEY CLUSTERED ([TipoCotacaoId] ASC);
GO

ALTER TABLE [dbo].[tipoenvio]
ADD CONSTRAINT [PK_tipoenvio] PRIMARY KEY CLUSTERED ([TipoEnvioId] ASC);
GO

ALTER TABLE [dbo].[socialmedia]
ADD CONSTRAINT [PK_socialmedia] PRIMARY KEY CLUSTERED ([SocialMediaId] ASC);
GO

ALTER TABLE [dbo].[dict_socialmedia]
ADD CONSTRAINT [PK_dict_socialmedia] PRIMARY KEY CLUSTERED ([SocialMediaId] ASC);
GO

ALTER TABLE [dbo].[aplicacao]
ADD CONSTRAINT [PK_aplicacao] PRIMARY KEY CLUSTERED ([AplicacaoId] ASC);
GO

ALTER TABLE [dbo].[areas]
ADD CONSTRAINT [PK_areas] PRIMARY KEY CLUSTERED ([AreaId] ASC);
GO

ALTER TABLE [dbo].[relimagemconteudo]
ADD CONSTRAINT [PK_relimagemconteudo] PRIMARY KEY CLUSTERED ([relid] ASC);
GO

ALTER TABLE [dbo].[templates]
ADD CONSTRAINT [PK_templates] PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Script concluido - 35 tabelas criadas
-- --------------------------------------------------
