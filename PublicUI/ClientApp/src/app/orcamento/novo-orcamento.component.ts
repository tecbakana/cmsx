import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

interface OpcaoPublica {
  opcaoid: string;
  nome: string;
  valorAdicional: number;
}

interface AtributoPublico {
  atributoid: string;
  nome: string;
  ordem: number;
  valorAdicional: number;
  opcoes: OpcaoPublica[];
  filhos: AtributoPublico[];
}

interface CheckboxAtributo {
  atributoid: string;
  label: string;
  valorAdicional: number;
  selecionado: boolean;
}

interface ProdutoPublico {
  produtoid: string;
  nome: string;
  valor: number | null;
  unidadeVenda: string | null;
  atributos: AtributoPublico[];
}

// ── Cascade selects ──────────────────────────────────────────────

interface CascadeItem {
  id: string;               // opcaoid se folha, atributoid se filho
  nome: string;
  valorAdicional: number;
  filhoNode?: AtributoPublico;
}

interface CascadeLevel {
  atributoid: string;
  label: string;
  isLeaf: boolean;
  itens: CascadeItem[];
  selecionado: string;
}

interface CadeiaCascata {
  levels: CascadeLevel[];
}

// ── Seleção final ────────────────────────────────────────────────

interface SelecaoComposta {
  atributoid: string;
  atributoNome: string;
  opcaoid: string;
  opcaoNome: string;
  valorAdicional: number;
}

interface CheckboxSelecionado {
  atributoid: string;
  atributoNome: string;
  valorAdicional: number;
}

interface ItemComposto {
  produtoid: string;
  produtoNome: string;
  quantidade: number;
  valorBase: number;
  selecoes: SelecaoComposta[];
  checkboxes: CheckboxSelecionado[];
  valorUnitario: number;
  valorTotal: number;
}

interface ItemOrcamento {
  descricao: string;
  quantidade: number;
  valor: number | null;
  ativo: boolean;
}

@Component({
  templateUrl: './novo-orcamento.component.html'
})
export class NovoOrcamentoComponent implements OnInit {
  token = '';
  enviando = false;
  sucesso = false;
  erro = '';
  erroSelecao = '';

  form = {
    nome: '',
    email: '',
    telefone: '',
    descricaoservico: '',
    valorestimado: null as number | null,
    prazo: '',
    nomevendedor: ''
  };

  itens: ItemOrcamento[] = [];
  itensCompostos: ItemComposto[] = [];

  produtos: ProdutoPublico[] = [];
  produtoSelecionadoId = '';
  cadeias: CadeiaCascata[] = [];
  checkboxAtribs: CheckboxAtributo[] = [];

  constructor(private http: HttpClient, private route: ActivatedRoute) {}

  ngOnInit() {
    this.token = this.route.snapshot.queryParamMap.get('ref') ?? '';
    if (this.token) this.carregarProdutos();
  }

  carregarProdutos() {
    this.http.get<ProdutoPublico[]>(`/api/publico/orcamento/produtos?ref=${this.token}`)
      .subscribe({ next: p => this.produtos = p });
  }

  get produtoSelecionado(): ProdutoPublico | null {
    return this.produtos.find(p => p.produtoid === this.produtoSelecionadoId) ?? null;
  }

  private buildLevel(node: AtributoPublico): CascadeLevel | null {
    if (node.filhos.length > 0) {
      return {
        atributoid: node.atributoid,
        label: node.nome,
        isLeaf: false,
        itens: node.filhos.map(f => ({
          id: f.atributoid,
          nome: f.nome,
          valorAdicional: f.valorAdicional ?? 0,
          filhoNode: f
        })),
        selecionado: ''
      };
    } else if (node.opcoes.length > 0) {
      return {
        atributoid: node.atributoid,
        label: node.nome,
        isLeaf: true,
        itens: node.opcoes.map(o => ({
          id: o.opcaoid,
          nome: o.nome,
          valorAdicional: o.valorAdicional ?? 0
        })),
        selecionado: ''
      };
    }
    return null;
  }

  onProdutoChange() {
    this.erroSelecao = '';
    this.cadeias = [];
    this.checkboxAtribs = [];
    const produto = this.produtoSelecionado;
    if (!produto) return;

    for (const atrib of produto.atributos) {
      const level = this.buildLevel(atrib);
      if (level) {
        this.cadeias.push({ levels: [level] });
      } else if ((atrib.valorAdicional ?? 0) > 0) {
        this.checkboxAtribs.push({
          atributoid: atrib.atributoid,
          label: atrib.nome,
          valorAdicional: atrib.valorAdicional,
          selecionado: false
        });
      }
    }
  }

  onLevelChange(cadeiaIdx: number, levelIdx: number) {
    const cadeia = this.cadeias[cadeiaIdx];
    const level = cadeia.levels[levelIdx];

    // Remove níveis subsequentes
    cadeia.levels = cadeia.levels.slice(0, levelIdx + 1);

    if (!level.selecionado || level.isLeaf) return;

    const item = level.itens.find(i => i.id === level.selecionado);
    if (!item?.filhoNode) return;

    const nextLevel = this.buildLevel(item.filhoNode);
    if (nextLevel) {
      cadeia.levels.push(nextLevel);
    } else if ((item.filhoNode.valorAdicional ?? 0) > 0) {
      const exists = this.checkboxAtribs.some(c => c.atributoid === item.filhoNode!.atributoid);
      if (!exists) {
        this.checkboxAtribs.push({
          atributoid: item.filhoNode.atributoid,
          label: item.filhoNode.nome,
          valorAdicional: item.filhoNode.valorAdicional,
          selecionado: false
        });
      }
    }
  }

  trackByIndex(index: number) { return index; }

  adicionarItem() {
    const produto = this.produtoSelecionado;
    if (!produto) return;

    const selecoes: SelecaoComposta[] = [];
    for (const cadeia of this.cadeias) {
      const lastLevel = cadeia.levels[cadeia.levels.length - 1];
      if (!lastLevel.isLeaf || !lastLevel.selecionado) {
        this.erroSelecao = 'Complete todas as seleções antes de adicionar.';
        return;
      }
      const item = lastLevel.itens.find(i => i.id === lastLevel.selecionado);
      if (!item) continue;
      selecoes.push({
        atributoid: lastLevel.atributoid,
        atributoNome: lastLevel.label,
        opcaoid: lastLevel.selecionado,
        opcaoNome: item.nome,
        valorAdicional: item.valorAdicional
      });
    }

    this.erroSelecao = '';

    const checkboxes: CheckboxSelecionado[] = this.checkboxAtribs
      .filter(c => c.selecionado)
      .map(c => ({ atributoid: c.atributoid, atributoNome: c.label, valorAdicional: c.valorAdicional }));

    const somaAdicionais = selecoes.reduce((s, sel) => s + sel.valorAdicional, 0);
    const somaCheckboxes = checkboxes.reduce((s, c) => s + c.valorAdicional, 0);
    const valorBase = produto.valor ?? 0;
    const valorUnitario = valorBase + somaAdicionais + somaCheckboxes;

    this.itensCompostos.push({
      produtoid: produto.produtoid,
      produtoNome: produto.nome,
      quantidade: 1,
      valorBase,
      selecoes,
      checkboxes,
      valorUnitario,
      valorTotal: valorUnitario
    });

    this.produtoSelecionadoId = '';
    this.cadeias = [];
    this.checkboxAtribs = [];
    this.atualizarTotal();
  }

  removeItemComposto(index: number) {
    this.itensCompostos.splice(index, 1);
    this.atualizarTotal();
  }

  onQuantidadeCompostoChange(item: ItemComposto) {
    item.valorTotal = item.valorUnitario * item.quantidade;
    this.atualizarTotal();
  }

  calcularValorLinha(item: ItemOrcamento): number {
    return (item.valor ?? 0) * item.quantidade;
  }

  atualizarTotal() {
    const totalCompostos = this.itensCompostos.reduce((s, i) => s + i.valorUnitario * i.quantidade, 0);
    const totalSimples = this.itens.reduce((s, i) => s + this.calcularValorLinha(i), 0);
    const total = totalCompostos + totalSimples;
    this.form.valorestimado = total > 0 ? total : null;
  }

  salvar() {
    this.erro = '';
    if (!this.token) { this.erro = 'Link inválido. Solicite um novo link ao administrador.'; return; }
    if (!this.form.nome.trim()) { this.erro = 'Nome do cliente é obrigatório.'; return; }
    if (this.itensCompostos.length === 0 && this.itens.filter(i => i.descricao.trim()).length === 0) {
      this.erro = 'Adicione pelo menos um item ao orçamento.'; return;
    }

    this.enviando = true;
    const payload = {
      token: this.token,
      ...this.form,
      itens: this.itens.filter(i => i.descricao.trim()),
      itensCompostos: this.itensCompostos.map(ic => ({
        produtoid: ic.produtoid,
        quantidade: ic.quantidade,
        selecoes: ic.selecoes.map(s => ({ atributoid: s.atributoid, opcaoid: s.opcaoid })),
        checkboxAtributos: ic.checkboxes.map(c => c.atributoid)
      }))
    };

    this.http.post('/api/publico/orcamento', payload).subscribe({
      next: () => { this.sucesso = true; this.enviando = false; },
      error: () => { this.erro = 'Erro ao salvar orçamento. Tente novamente.'; this.enviando = false; }
    });
  }

  novoOrcamento() {
    this.sucesso = false;
    this.form = { nome: '', email: '', telefone: '', descricaoservico: '', valorestimado: null, prazo: '', nomevendedor: '' };
    this.itens = [];
    this.itensCompostos = [];
    this.erro = '';
    this.erroSelecao = '';
    this.produtoSelecionadoId = '';
    this.cadeias = [];
    this.checkboxAtribs = [];
  }
}
