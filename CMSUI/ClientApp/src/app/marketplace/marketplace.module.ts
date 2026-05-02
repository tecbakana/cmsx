import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { PedidosListaComponent } from './pedidos-lista/pedidos-lista.component';

@NgModule({
  declarations: [PedidosListaComponent],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild([
      { path: '', component: PedidosListaComponent }
    ])
  ]
})
export class MarketplaceModule {}
