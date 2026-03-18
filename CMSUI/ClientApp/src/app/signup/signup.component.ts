import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({ templateUrl: './signup.component.html' })
export class SignupComponent {
  dados = {
    nome: '', sobrenome: '', apelido: '', senha: '', confirmarSenha: '',
    appNome: '', appUrl: ''
  };
  erro = '';
  sucesso = '';
  carregando = false;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private router: Router
  ) {
    if (sessionStorage.getItem('usuario')) this.router.navigate(['/']);
  }

  cadastrar() {
    this.erro = '';
    if (this.dados.senha !== this.dados.confirmarSenha) {
      this.erro = 'As senhas não conferem.'; return;
    }
    if (!this.dados.apelido || this.dados.apelido.length > 6) {
      this.erro = 'Login deve ter até 6 caracteres.'; return;
    }
    this.carregando = true;
    this.http.post(this.baseUrl + 'auth/signup', this.dados).subscribe({
      next: () => {
        this.sucesso = 'Cadastro realizado! Aguarde a ativação ou faça login.';
        this.carregando = false;
      },
      error: (err) => {
        this.erro = err.error?.message || 'Erro ao realizar cadastro.';
        this.carregando = false;
      }
    });
  }
}
