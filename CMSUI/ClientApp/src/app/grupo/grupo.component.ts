import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({ templateUrl: './grupo.component.html' })
export class GrupoComponent implements OnInit {
  lista: any[] = [];
  todos_usuarios: any[] = [];
  selecionado: any = null;
  membros: any[] = [];
  modoEdicao = false;
  novoUsuarioid = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  ngOnInit() {
    this.carregar();
    this.http.get<any[]>(this.baseUrl + 'usuarios').subscribe(r => this.todos_usuarios = r);
  }

  carregar() {
    this.http.get<any[]>(this.baseUrl + 'grupos').subscribe(r => this.lista = r);
  }

  novo() {
    this.selecionado = { nome: '', descricao: '', acessototal: false };
    this.membros = [];
    this.modoEdicao = true;
  }

  editar(item: any) {
    this.selecionado = { ...item };
    this.modoEdicao = true;
    this.http.get<any[]>(this.baseUrl + 'grupos/' + item.grupoid + '/usuarios')
      .subscribe(r => this.membros = r);
  }

  salvar() {
    if (this.selecionado.grupoid) {
      this.http.put(this.baseUrl + 'grupos/' + this.selecionado.grupoid, this.selecionado)
        .subscribe(() => { this.modoEdicao = false; this.carregar(); });
    } else {
      this.http.post(this.baseUrl + 'grupos', this.selecionado)
        .subscribe(() => { this.modoEdicao = false; this.carregar(); });
    }
  }

  excluir(id: string) {
    if (confirm('Excluir este grupo? Os vínculos de usuários serão removidos.')) {
      this.http.delete(this.baseUrl + 'grupos/' + id).subscribe(() => this.carregar());
    }
  }

  adicionarUsuario() {
    if (!this.novoUsuarioid) return;
    this.http.post(this.baseUrl + 'grupos/' + this.selecionado.grupoid + '/usuarios',
      JSON.stringify(this.novoUsuarioid),
      { headers: { 'Content-Type': 'application/json' } }
    ).subscribe({
      next: () => {
        this.novoUsuarioid = '';
        this.http.get<any[]>(this.baseUrl + 'grupos/' + this.selecionado.grupoid + '/usuarios')
          .subscribe(r => this.membros = r);
      },
      error: (err) => alert(err.error?.message || 'Erro ao adicionar usuário.')
    });
  }

  removerUsuario(relacaoid: string) {
    if (confirm('Remover usuário do grupo?')) {
      this.http.delete(this.baseUrl + 'grupos/' + this.selecionado.grupoid + '/usuarios/' + relacaoid)
        .subscribe(() => {
          this.membros = this.membros.filter(m => m.relacaoid !== relacaoid);
        });
    }
  }

  cancelar() { this.modoEdicao = false; this.selecionado = null; this.membros = []; }
}
