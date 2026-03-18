import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AdminContextService } from '../admin-context.service';

@Component({ templateUrl: './produto.component.html' })
export class ProdutoComponent implements OnInit {
  lista: any[] = [];
  categorias: any[] = [];
  selecionado: any = null;
  modoEdicao = false;
  aba: 'geral' | 'atributos' | 'galeria' = 'geral';
  private usuario: any;

  // Atributos
  atributos: any[] = [];
  novoAtrib = { nome: '', descricao: '' };
  novaOpcao: { [atributoid: string]: { nome: string; qtd: number; estoque: number } } = {};

  // Galeria
  imagens: any[] = [];
  novaImagem = { url: '', descricao: '' };

  // IA
  iaImageUrl = '';
  iaProvedor = '';
  iaGerando = false;
  iaErro = '';
  iaArquivoNome = '';
  iaImagemPreview = '';
  iaAtributos: { nome: string; selecionado: boolean; opcoes: { nome: string; estoque: number; selecionado: boolean }[] }[] = [];
  iaAplicando = false;
  private iaArquivo: File | null = null;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private adminCtx: AdminContextService) {}

  ngOnInit() {
    this.usuario = JSON.parse(sessionStorage.getItem('usuario') || '{}');
    this.adminCtx.tenant$.subscribe(() => {
      this.carregar();
      this.carregarCategorias();
    });
  }

  private appParams(): HttpParams {
    let p = new HttpParams();
    if (!this.usuario.acessoTotal && this.usuario.aplicacaoid)
      p = p.set('aplicacaoid', this.usuario.aplicacaoid);
    else if (this.usuario.acessoTotal && this.adminCtx.tenantId)
      p = p.set('aplicacaoid', this.adminCtx.tenantId);
    return p;
  }

  carregarCategorias() {
    this.http.get<any[]>(this.baseUrl + 'categorias', { params: this.appParams() })
      .subscribe(r => this.categorias = r);
  }

  carregar() {
    this.http.get<any[]>(this.baseUrl + 'produtos', { params: this.appParams() })
      .subscribe(r => this.lista = r);
  }

  novo() {
    this.selecionado = {
      nome: '', descricao: '', descricacurta: '', detalhetecnico: '', pagsegurokey: '',
      valor: null, sku: '', tipo: null, destaque: 0,
      aplicacaoid: this.usuario.aplicacaoid
    };
    this.atributos = []; this.imagens = [];
    this.aba = 'geral'; this.modoEdicao = true;
  }

  editar(item: any) {
    this.selecionado = { ...item };
    this.aba = 'geral'; this.modoEdicao = true;
    this.carregarAtributos();
    this.carregarImagens();
  }

  salvarGeral() {
    if (this.selecionado.produtoid) {
      this.http.put(this.baseUrl + 'produtos/' + this.selecionado.produtoid, this.selecionado)
        .subscribe(() => this.carregar());
    } else {
      this.http.post(this.baseUrl + 'produtos', this.selecionado)
        .subscribe((r: any) => {
          this.selecionado = r;
          this.carregar();
        });
    }
  }

  excluir(id: string) {
    if (confirm('Excluir este produto?'))
      this.http.delete(this.baseUrl + 'produtos/' + id).subscribe(() => this.carregar());
  }

  cancelar() { this.modoEdicao = false; this.selecionado = null; this.atributos = []; this.imagens = []; }

  // ── IA ─────────────────────────────────────────────────────────────

  onArquivoSelecionado(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (!file) return;
    this.iaArquivo = file;
    this.iaArquivoNome = file.name;
  }

  limparArquivo() {
    this.iaArquivo = null;
    this.iaArquivoNome = '';
  }

  gerarDescricao() {
    if (!this.iaArquivo && !this.iaImageUrl.trim()) {
      this.iaErro = 'Informe uma URL ou faça upload de uma imagem.';
      return;
    }
    this.iaGerando = true;
    this.iaErro = '';

    const form = new FormData();
    if (this.iaArquivo) {
      form.append('arquivo', this.iaArquivo);
    } else {
      form.append('imageUrl', this.iaImageUrl);
    }
    if (this.iaProvedor) form.append('provedor', this.iaProvedor);
    if (this.selecionado?.produtoid) form.append('produtoid', this.selecionado.produtoid);

    this.http.post<any>(this.baseUrl + 'produtos/gerar-descricao', form).subscribe({
      next: r => {
        this.iaGerando = false;
        if (r.nome)           this.selecionado.nome           = r.nome;
        if (r.descricacurta)  this.selecionado.descricacurta  = r.descricacurta;
        if (r.descricao)      this.selecionado.descricao       = r.descricao;
        if (r.detalhetecnico) this.selecionado.detalhetecnico  = r.detalhetecnico;
        if (r.imagemUrl)      this.iaImagemPreview             = r.imagemUrl;

        if (r.atributos && r.atributos.length > 0) {
          this.iaAtributos = r.atributos.map((a: any) => ({
            nome: a.nome,
            selecionado: true,
            opcoes: (a.opcoes ?? []).map((o: any) => ({ nome: o.nome, estoque: o.estoque ?? 0, selecionado: true }))
          }));
        } else {
          this.iaAtributos = [];
        }

        // Recarrega galeria se já salvou na galeria
        if (this.selecionado?.produtoid) this.carregarImagens();
      },
      error: e => {
        this.iaGerando = false;
        this.iaErro = e?.error ?? 'Erro ao chamar o agente IA.';
      }
    });
  }

  aplicarAtributos() {
    if (!this.selecionado?.produtoid) {
      this.iaErro = 'Salve o produto primeiro para aplicar os atributos.';
      return;
    }
    const selecionados = this.iaAtributos.filter(a => a.selecionado);
    if (selecionados.length === 0) return;

    this.iaAplicando = true;
    const criarProximo = (i: number) => {
      if (i >= selecionados.length) { this.iaAplicando = false; this.iaAtributos = []; this.carregarAtributos(); return; }
      const a = selecionados[i];
      this.http.post<any>(this.baseUrl + 'produtos/' + this.selecionado.produtoid + '/atributos', { nome: a.nome, descricao: '' })
        .subscribe(atribCriado => {
          const opcoesSel = a.opcoes.filter(o => o.selecionado);
          const criarOpcao = (j: number) => {
            if (j >= opcoesSel.length) { criarProximo(i + 1); return; }
            const o = opcoesSel[j];
            this.http.post(this.baseUrl + 'produtos/' + this.selecionado.produtoid + '/atributos/' + atribCriado.atributoid + '/opcoes',
              { nome: o.nome, qtd: 1, estoque: o.estoque })
              .subscribe(() => criarOpcao(j + 1));
          };
          criarOpcao(0);
        });
    };
    criarProximo(0);
  }

  // ── Atributos ──────────────────────────────────────────────────────

  carregarAtributos() {
    if (!this.selecionado?.produtoid) return;
    this.http.get<any[]>(this.baseUrl + 'produtos/' + this.selecionado.produtoid + '/atributos')
      .subscribe(r => {
        this.atributos = r;
        r.forEach((a: any) => {
          if (!this.novaOpcao[a.atributoid])
            this.novaOpcao[a.atributoid] = { nome: '', qtd: 1, estoque: 0 };
        });
      });
  }

  adicionarAtributo() {
    if (!this.novoAtrib.nome.trim()) return;
    this.http.post(this.baseUrl + 'produtos/' + this.selecionado.produtoid + '/atributos', this.novoAtrib)
      .subscribe(() => { this.novoAtrib = { nome: '', descricao: '' }; this.carregarAtributos(); });
  }

  removerAtributo(atributoid: string) {
    if (confirm('Remover atributo e todas as suas opções?'))
      this.http.delete(this.baseUrl + 'produtos/' + this.selecionado.produtoid + '/atributos/' + atributoid)
        .subscribe(() => this.carregarAtributos());
  }

  adicionarOpcao(atributoid: string) {
    const o = this.novaOpcao[atributoid];
    if (!o?.nome?.trim()) return;
    this.http.post(
      this.baseUrl + 'produtos/' + this.selecionado.produtoid + '/atributos/' + atributoid + '/opcoes', o)
      .subscribe(() => { o.nome = ''; o.qtd = 1; o.estoque = 0; this.carregarAtributos(); });
  }

  removerOpcao(atributoid: string, opcaoid: string) {
    this.http.delete(
      this.baseUrl + 'produtos/' + this.selecionado.produtoid + '/atributos/' + atributoid + '/opcoes/' + opcaoid)
      .subscribe(() => this.carregarAtributos());
  }

  // ── Galeria ────────────────────────────────────────────────────────

  carregarImagens() {
    if (!this.selecionado?.produtoid) return;
    this.http.get<any[]>(this.baseUrl + 'produtos/' + this.selecionado.produtoid + '/imagens')
      .subscribe(r => this.imagens = r);
  }

  adicionarImagem() {
    if (!this.novaImagem.url.trim()) return;
    this.http.post(this.baseUrl + 'produtos/' + this.selecionado.produtoid + '/imagens', this.novaImagem)
      .subscribe(() => { this.novaImagem = { url: '', descricao: '' }; this.carregarImagens(); });
  }

  removerImagem(imagemid: string) {
    this.http.delete(this.baseUrl + 'produtos/' + this.selecionado.produtoid + '/imagens/' + imagemid)
      .subscribe(() => this.carregarImagens());
  }
}
