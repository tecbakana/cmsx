import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AdminContextService } from '../admin-context.service';

@Component({ templateUrl: './usuario.component.html' })
export class UsuarioComponent implements OnInit {
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
    this.http.get<any[]>(this.baseUrl + 'usuarios', { params: this.params() })
      .subscribe(r => this.lista = r);
  }

  novo() {
    this.selecionado = { nome: '', sobrenome: '', apelido: '', senha: '', ativo: 1 };
    this.modoEdicao = true;
  }

  editar(item: any) {
    this.selecionado = { ...item, senha: '' };
    this.modoEdicao = true;
  }

  salvar() {
    if (this.selecionado.userid) {
      this.http.put(this.baseUrl + 'usuarios/' + this.selecionado.userid, this.selecionado)
        .subscribe(() => { this.modoEdicao = false; this.carregar(); });
    } else {
      this.http.post(this.baseUrl + 'usuarios', this.selecionado)
        .subscribe(() => { this.modoEdicao = false; this.carregar(); });
    }
  }

  excluir(id: string) {
    if (confirm('Excluir usuário?')) {
      this.http.delete(this.baseUrl + 'usuarios/' + id).subscribe(() => this.carregar());
    }
  }

  cancelar() { this.modoEdicao = false; this.selecionado = null; }
}
