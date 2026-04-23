import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({ selector: 'app-landing', template: '' })
export class LandingComponent {
  constructor(private router: Router) {
    const usuario = sessionStorage.getItem('usuario');
    if (usuario) {
      this.router.navigate(['/dashboard']);
    } else {
      this.router.navigate(['/s/multiplai']);
    }
  }
}
