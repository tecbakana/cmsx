import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Component({ templateUrl: './vinculo-modulo.component.html' })
export class VinculoModuloComponent implements OnInit {
  vinculos: any[] = [];
  usuarios: any[] = [];
  modulos: any[] = [];
  usuarioid = '';
  moduloid = '';
  erro = '';
  filtroUsuario = '';
  private usuario: any;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  ngOnInit() {
    this.usuario = JSON.parse(sessionStorage.getItem('usuario') || '{}');
    this.carregarUsuarios();
    this.http.get<any[]>(this.baseUrl + 'modulos').subscribe(r => this.modulos = r);
    this.carregar();
  }

  private appParams(): HttpParams {
    let p = new HttpParams();
    if (!this.usuario.acessoTotal && this.usuario.aplicacaoid)
      p = p.set('aplicacaoid', this.usuario.aplicacaoid);
    return p;
  }

  carregarUsuarios() {
    this.http.get<any[]>(this.baseUrl + 'usuarios', { params: this.appParams() })
      .subscribe(r => this.usuarios = r);
  }

  carregar() {
    let p = this.appParams();
    if (this.filtroUsuario) p = p.set('usuarioid', this.filtroUsuario);
    this.http.get<any[]>(this.baseUrl + 'vinculosmodulo', { params: p })
      .subscribe(r => this.vinculos = r);
  }

  vincular() {
    this.erro = '';
    if (!this.usuarioid || !this.moduloid) {
      this.erro = 'Selecione usuário e módulo.'; return;
    }
    this.http.post(this.baseUrl + 'vinculosmodulo', { usuarioid: this.usuarioid, moduloid: this.moduloid })
      .subscribe({
        next: () => { this.usuarioid = ''; this.moduloid = ''; this.carregar(); },
        error: (err) => this.erro = err.error?.message || 'Erro ao vincular.'
      });
  }

  desvincular(relacaoid: string, nome: string) {
    if (confirm('Remover acesso de ' + nome + ' a este módulo?'))
      this.http.delete(this.baseUrl + 'vinculosmodulo/' + relacaoid).subscribe(() => this.carregar());
  }
}
