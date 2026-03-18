import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AdminContextService } from '../admin-context.service';

declare var Quill: any;

@Component({ templateUrl: './conteudo.component.html' })
export class ConteudoComponent implements OnInit {
  lista: any[] = [];
  areas: any[] = [];
  categorias: any[] = [];
  selecionado: any = null;
  modoEdicao = false;
  private usuario: any;
  private quill: any = null;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private adminCtx: AdminContextService) {}

  ngOnInit() {
    this.usuario = JSON.parse(sessionStorage.getItem('usuario') || '{}');
    this.adminCtx.tenant$.subscribe(() => {
      this.carregarAreas();
      this.carregarCategorias();
      this.carregar();
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

  carregar() {
    this.http.get<any[]>(this.baseUrl + 'conteudos', { params: this.appParams() })
      .subscribe(r => this.lista = r);
  }

  private iniciarEditor() {
    setTimeout(() => {
      const el = document.getElementById('quill-editor');
      if (!el) return;
      this.quill = new Quill(el, {
        theme: 'snow',
        modules: {
          toolbar: [
            [{ header: [1, 2, 3, false] }],
            ['bold', 'italic', 'underline', 'strike'],
            [{ list: 'ordered' }, { list: 'bullet' }],
            ['blockquote', 'code-block', 'link'],
            [{ color: [] }, { background: [] }],
            ['clean']
          ]
        }
      });
      if (this.selecionado?.texto) {
        this.quill.root.innerHTML = this.selecionado.texto;
      }
    }, 0);
  }

  novo() {
    this.selecionado = { titulo: '', texto: '', autor: this.usuario.nome || '', areaid: null, cateriaid: null };
    this.modoEdicao = true;
    this.quill = null;
    this.iniciarEditor();
  }

  editar(item: any) {
    this.selecionado = { ...item };
    this.modoEdicao = true;
    this.quill = null;
    this.iniciarEditor();
  }

  salvar() {
    if (this.quill) {
      this.selecionado.texto = this.quill.root.innerHTML;
    }
    if (this.selecionado.conteudoid) {
      this.http.put(this.baseUrl + 'conteudos/' + this.selecionado.conteudoid, this.selecionado)
        .subscribe(() => { this.modoEdicao = false; this.quill = null; this.carregar(); });
    } else {
      this.http.post(this.baseUrl + 'conteudos', this.selecionado)
        .subscribe(() => { this.modoEdicao = false; this.quill = null; this.carregar(); });
    }
  }

  excluir(id: string) {
    if (confirm('Excluir este conteúdo?'))
      this.http.delete(this.baseUrl + 'conteudos/' + id).subscribe(() => this.carregar());
  }

  cancelar() { this.modoEdicao = false; this.selecionado = null; this.quill = null; }
}
