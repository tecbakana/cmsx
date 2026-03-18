import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AdminContextService } from '../admin-context.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html'
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  modulos: any[] = [];
  usuario: any = null;
  aplicacoes: any[] = [];
  tenantSelecionadoId = '';

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    public adminCtx: AdminContextService
  ) {}

  ngOnInit() {
    this.usuario = JSON.parse(sessionStorage.getItem('usuario') || 'null');
    if (this.usuario) {
      const url = this.usuario.acessoTotal
        ? this.baseUrl + 'modulos'
        : this.baseUrl + 'modulos?usuarioid=' + this.usuario.userid;
      this.http.get<any[]>(url).subscribe(m => this.modulos = m);

      if (this.usuario.acessoTotal) {
        this.http.get<any[]>(this.baseUrl + 'aplicacaos').subscribe(a => this.aplicacoes = a);
        this.tenantSelecionadoId = this.adminCtx.tenantId ?? '';
      }
    }
  }

  onTenantChange() {
    const app = this.aplicacoes.find(a => a.aplicacaoid === this.tenantSelecionadoId);
    this.adminCtx.set(app ? { aplicacaoid: app.aplicacaoid, nome: app.nome } : null);
  }

  toggle() { this.isExpanded = !this.isExpanded; }

  logout() {
    sessionStorage.removeItem('usuario');
    sessionStorage.removeItem('adminTenant');
    window.location.href = '/login';
  }
}
