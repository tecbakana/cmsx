import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({ templateUrl: './vinculo.component.html' })
export class VinculoComponent implements OnInit {
  vinculos: any[] = [];
  usuarios: any[] = [];
  aplicacoes: any[] = [];
  usuarioid = '';
  aplicacaoid = '';
  erro = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  ngOnInit() {
    this.carregar();
    this.http.get<any[]>(this.baseUrl + 'usuarios').subscribe(r => this.usuarios = r);
    this.http.get<any[]>(this.baseUrl + 'aplicacaos').subscribe(r => this.aplicacoes = r);
  }

  carregar() {
    this.http.get<any[]>(this.baseUrl + 'vinculos').subscribe(r => this.vinculos = r);
  }

  filtrarPorApp(id: string) {
    if (!id) { this.carregar(); return; }
    this.http.get<any[]>(this.baseUrl + 'vinculos?aplicacaoid=' + id)
      .subscribe(r => this.vinculos = r);
  }

  vincular() {
    this.erro = '';
    if (!this.usuarioid || !this.aplicacaoid) {
      this.erro = 'Selecione usuário e aplicação.'; return;
    }
    this.http.post(this.baseUrl + 'vinculos', { usuarioid: this.usuarioid, aplicacaoid: this.aplicacaoid })
      .subscribe({
        next: () => { this.usuarioid = ''; this.aplicacaoid = ''; this.carregar(); },
        error: (err) => this.erro = err.error?.message || 'Erro ao vincular.'
      });
  }

  desvincular(relacaoid: string, nome: string) {
    if (confirm('Remover vínculo de ' + nome + '?')) {
      this.http.delete(this.baseUrl + 'vinculos/' + relacaoid).subscribe(() => this.carregar());
    }
  }
}
