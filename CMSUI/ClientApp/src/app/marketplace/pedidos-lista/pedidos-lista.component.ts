import { Component, OnInit } from '@angular/core';
import { MarketplaceApiService } from '../marketplace-api.service';

@Component({ templateUrl: './pedidos-lista.component.html' })
export class PedidosListaComponent implements OnInit {
  pedidos: any[] = [];
  carregando = false;

  filtroMarketplace = '';
  filtroStatusNf = '';
  filtroBusca = '';

  pedidoSelecionado: any = null;
  carregandoDetalhe = false;

  emitindoNf = false;
  modalAberto = false;
  resultadoEmissao: any = null;

  readonly statusNfLabels: Record<string, string> = {
    nao_emitida: 'Não emitida',
    emitindo:    'Emitindo...',
    emitida:     'Emitida',
    erro:        'Erro',
  };

  readonly statusNfClasses: Record<string, string> = {
    nao_emitida: 'bg-secondary',
    emitindo:    'bg-warning text-dark',
    emitida:     'bg-success',
    erro:        'bg-danger',
  };

  constructor(private api: MarketplaceApiService) {}

  ngOnInit() { this.carregar(); }

  carregar() {
    this.carregando = true;
    this.api.getPedidos().subscribe({
      next: r => { this.pedidos = r; this.carregando = false; },
      error: () => { this.carregando = false; }
    });
  }

  get pedidosFiltrados(): any[] {
    return this.pedidos.filter(p => {
      const matchMp = !this.filtroMarketplace || p.marketplace === this.filtroMarketplace;
      const matchNf = !this.filtroStatusNf || p.statusNF === this.filtroStatusNf;
      const matchBusca = !this.filtroBusca ||
        p.numeroPedido?.toLowerCase().includes(this.filtroBusca.toLowerCase());
      return matchMp && matchNf && matchBusca;
    });
  }

  get marketplacesDisponiveis(): string[] {
    return [...new Set(this.pedidos.map(p => p.marketplace))];
  }

  get isMobile(): boolean {
    return window.innerWidth < 768;
  }

  selecionarPedido(pedido: any) {
    if (this.pedidoSelecionado?.id === pedido.id) {
      this.pedidoSelecionado = null;
      return;
    }
    this.carregandoDetalhe = true;
    this.pedidoSelecionado = null;
    this.api.getPedido(pedido.id).subscribe({
      next: r => { this.pedidoSelecionado = r; this.carregandoDetalhe = false; },
      error: () => { this.carregandoDetalhe = false; }
    });
  }

  fecharDetalhe() { this.pedidoSelecionado = null; }

  abrirModalEmissao() {
    this.resultadoEmissao = null;
    this.emitindoNf = true;
    this.modalAberto = true;
    this.api.emitirNf(this.pedidoSelecionado.id).subscribe({
      next: r => {
        this.emitindoNf = false;
        this.resultadoEmissao = r;
        this.carregar();
      },
      error: () => {
        this.emitindoNf = false;
        this.resultadoEmissao = { sucesso: false, erro: 'Erro de comunicação' };
      }
    });
  }

  fecharModal() { this.modalAberto = false; }
}
