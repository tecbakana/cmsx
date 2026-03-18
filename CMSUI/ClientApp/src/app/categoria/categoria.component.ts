import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AdminContextService } from '../admin-context.service';

@Component({ templateUrl: './categoria.component.html' })
export class CategoriaComponent implements OnInit {
  lista: any[] = [];
  selecionado: any = null;
  modoEdicao = false;
  private usuario: any;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private adminCtx: AdminContextService) {}

  ngOnInit() {
    this.usuario = JSON.parse(sessionStorage.getItem('usuario') || '{}');
    this.adminCtx.tenant$.subscribe(() => this.carregar());
  }

  private params(): HttpParams {
    let p = new HttpParams();
    if (!this.usuario.acessoTotal && this.usuario.aplicacaoid)
      p = p.set('aplicacaoid', this.usuario.aplicacaoid);
    else if (this.usuario.acessoTotal && this.adminCtx.tenantId)
      p = p.set('aplicacaoid', this.adminCtx.tenantId);
    return p;
  }

  carregar() {
    this.http.get<any[]>(this.baseUrl + 'categorias', { params: this.params() })
      .subscribe(r => this.lista = r);
  }

  nomePai(cateriaidpai: string | null): string {
    if (!cateriaidpai) return '—';
    const pai = this.lista.find(c => c.cateriaid === cateriaidpai);
    return pai ? pai.nome : cateriaidpai;
  }

  opcoesPai(): any[] {
    if (!this.selecionado?.cateriaid) return this.lista;
    return this.lista.filter(c => c.cateriaid !== this.selecionado.cateriaid);
  }

  novo() {
    this.selecionado = {
      nome: '', descricao: '', tipocateria: null, cateriaidpai: null,
      aplicacaoid: this.usuario.aplicacaoid
    };
    this.modoEdicao = true;
  }

  editar(item: any) { this.selecionado = { ...item }; this.modoEdicao = true; }

  salvar() {
    if (this.selecionado.cateriaid) {
      this.http.put(this.baseUrl + 'categorias/' + this.selecionado.cateriaid, this.selecionado)
        .subscribe(() => { this.modoEdicao = false; this.carregar(); });
    } else {
      this.http.post(this.baseUrl + 'categorias', this.selecionado)
        .subscribe(() => { this.modoEdicao = false; this.carregar(); });
    }
  }

  excluir(id: string) {
    if (confirm('Excluir esta categoria?'))
      this.http.delete(this.baseUrl + 'categorias/' + id).subscribe(() => this.carregar());
  }

  cancelar() { this.modoEdicao = false; this.selecionado = null; }
}
