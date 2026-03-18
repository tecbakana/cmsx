import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AdminContextService } from '../admin-context.service';

interface Campo { label: string; tipo: string; obrigatorio: boolean; opcoes?: string; }

@Component({ templateUrl: './formulario.component.html' })
export class FormularioComponent implements OnInit {
  aba: 'defs' | 'respostas' | 'faq' = 'defs';

  // ── Definições ──────────────────────────────────────────────────────
  defs: any[] = [];
  areas: any[] = [];
  categorias: any[] = [];
  defSelecionada: any = null;
  modoEdicao = false;
  campos: Campo[] = [];
  private usuario: any;

  // ── Respostas ───────────────────────────────────────────────────────
  respostas: any[] = [];
  respostaSelecionada: any = null;
  promovendo: any = null;

  // ── FAQ ─────────────────────────────────────────────────────────────
  faqs: any[] = [];
  faqFormularioid: string | null = null;
  faqSelecionado: any = null;
  modoEdicaoFaq = false;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private adminCtx: AdminContextService) {}

  ngOnInit() {
    this.usuario = JSON.parse(sessionStorage.getItem('usuario') || '{}');
    this.adminCtx.tenant$.subscribe(() => {
      this.carregarAreas();
      this.carregarCategorias();
      this.carregarDefs();
      this.carregarRespostas();
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

  carregarAreas() {
    this.http.get<any[]>(this.baseUrl + 'areas', { params: this.appParams() })
      .subscribe(r => this.areas = r);
  }

  carregarCategorias() {
    this.http.get<any[]>(this.baseUrl + 'categorias', { params: this.appParams() })
      .subscribe(r => this.categorias = r);
  }

  // ── Definições ──────────────────────────────────────────────────────

  carregarDefs() {
    this.http.get<any[]>(this.baseUrl + 'formularios/defs', { params: this.appParams() })
      .subscribe(r => this.defs = r);
  }

  novaDef() {
    this.defSelecionada = { nome: '', areaid: null, categoriaid: null };
    this.campos = [];
    this.modoEdicao = true;
  }

  editarDef(item: any) {
    this.defSelecionada = { ...item };
    try { this.campos = JSON.parse(item.valor || '[]'); } catch { this.campos = []; }
    this.modoEdicao = true;
  }

  adicionarCampo() {
    this.campos.push({ label: '', tipo: 'text', obrigatorio: false });
  }

  removerCampo(i: number) { this.campos.splice(i, 1); }

  moverCampo(i: number, dir: -1 | 1) {
    const j = i + dir;
    if (j < 0 || j >= this.campos.length) return;
    [this.campos[i], this.campos[j]] = [this.campos[j], this.campos[i]];
  }

  salvarDef() {
    const payload = {
      nome: this.defSelecionada.nome,
      areaid: this.defSelecionada.areaid,
      categoriaid: this.defSelecionada.categoriaid,
      valor: JSON.stringify(this.campos)
    };
    if (this.defSelecionada.formularioid) {
      this.http.put(this.baseUrl + 'formularios/defs/' + this.defSelecionada.formularioid, payload)
        .subscribe(() => { this.modoEdicao = false; this.carregarDefs(); });
    } else {
      this.http.post(this.baseUrl + 'formularios/defs', payload)
        .subscribe(() => { this.modoEdicao = false; this.carregarDefs(); });
    }
  }

  excluirDef(id: string) {
    if (confirm('Excluir este formulário?'))
      this.http.delete(this.baseUrl + 'formularios/defs/' + id).subscribe(() => this.carregarDefs());
  }

  cancelar() { this.modoEdicao = false; this.defSelecionada = null; this.campos = []; }

  nomeArea(areaid: string): string {
    const a = this.areas.find(x => x.areaid === areaid);
    return a ? a.nome : areaid || '—';
  }

  nomeCategoria(cateriaid: string): string {
    const c = this.categorias.find(x => x.cateriaid === cateriaid);
    return c ? c.nome : '—';
  }

  parseCampos(valor: string): Campo[] {
    try { return JSON.parse(valor || '[]'); } catch { return []; }
  }

  // ── Respostas ───────────────────────────────────────────────────────

  carregarRespostas() {
    this.http.get<any[]>(this.baseUrl + 'formularios/respostas', { params: this.appParams() })
      .subscribe(r => this.respostas = r);
  }

  verResposta(r: any) { this.respostaSelecionada = r; }
  fecharResposta() { this.respostaSelecionada = null; this.promovendo = null; }

  excluirResposta(id: number) {
    if (confirm('Excluir esta resposta?'))
      this.http.delete(this.baseUrl + 'formularios/respostas/' + id)
        .subscribe(() => { this.respostaSelecionada = null; this.carregarRespostas(); });
  }

  iniciarPromocao(r: any) {
    this.promovendo = {
      idform: r.idform,
      formularioid: r.formularioid,
      pergunta: '',
      resposta: this.textoResposta(r),
      ordem: 0
    };
  }

  confirmarPromocao() {
    this.http.post(this.baseUrl + 'faq/promover/' + this.promovendo.idform, {
      pergunta: this.promovendo.pergunta,
      resposta: this.promovendo.resposta,
      ordem: this.promovendo.ordem
    }).subscribe(() => {
      this.promovendo = null;
      alert('Promovido para FAQ com sucesso!');
    });
  }

  textoResposta(r: any): string {
    try {
      const obj = JSON.parse(r.texto || '{}');
      return Object.entries(obj).map(([k, v]) => `${k}: ${v}`).join('\n');
    } catch {
      return r.texto || '';
    }
  }

  nomeFormulario(formularioid: string): string {
    const f = this.defs.find(x => x.formularioid === formularioid);
    return f ? f.nome : '—';
  }

  // ── FAQ ─────────────────────────────────────────────────────────────

  abrirFaq(formularioid: string) {
    this.faqFormularioid = formularioid;
    this.aba = 'faq';
    this.carregarFaqs();
  }

  carregarFaqs() {
    if (!this.faqFormularioid) return;
    this.http.get<any[]>(this.baseUrl + 'faq/' + this.faqFormularioid)
      .subscribe(r => this.faqs = r);
  }

  novoFaq() {
    this.faqSelecionado = { pergunta: '', resposta: '', ordem: 0 };
    this.modoEdicaoFaq = true;
  }

  editarFaq(item: any) {
    this.faqSelecionado = { ...item };
    this.modoEdicaoFaq = true;
  }

  salvarFaq() {
    const payload = {
      pergunta: this.faqSelecionado.pergunta,
      resposta: this.faqSelecionado.resposta,
      ordem: this.faqSelecionado.ordem
    };
    if (this.faqSelecionado.faqid) {
      this.http.put(this.baseUrl + 'faq/' + this.faqSelecionado.faqid, payload)
        .subscribe(() => { this.modoEdicaoFaq = false; this.carregarFaqs(); });
    } else {
      this.http.post(this.baseUrl + 'faq/' + this.faqFormularioid, payload)
        .subscribe(() => { this.modoEdicaoFaq = false; this.carregarFaqs(); });
    }
  }

  toggleAtivoFaq(item: any) {
    this.http.patch(this.baseUrl + 'faq/' + item.faqid + '/ativo', !item.ativo)
      .subscribe(() => { item.ativo = !item.ativo; });
  }

  excluirFaq(id: string) {
    if (confirm('Excluir este item do FAQ?'))
      this.http.delete(this.baseUrl + 'faq/' + id).subscribe(() => this.carregarFaqs());
  }

  cancelarFaq() { this.modoEdicaoFaq = false; this.faqSelecionado = null; }

  voltarParaDefs() {
    this.aba = 'defs';
    this.faqFormularioid = null;
    this.faqs = [];
  }
}
