import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  template: `
    <div class="container-fluid py-5 text-center">
      <h2 class="text-muted">Em construção</h2>
      <p class="text-muted">Esta seção ainda está sendo desenvolvida.</p>
      <a routerLink="/" class="btn btn-outline-secondary btn-sm">Voltar ao Dashboard</a>
    </div>
  `
})
export class EmConstrucaoComponent {}
