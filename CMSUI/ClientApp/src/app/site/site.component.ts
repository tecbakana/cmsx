import { Component, Inject, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { HttpClient } from '@angular/common/http';

@Component({ templateUrl: './site.component.html' })
export class SiteComponent implements OnInit, OnDestroy {
  site: any = null;
  carregando = true;
  erro = '';
  modo: 'slug' | 'preview' = 'slug';
  areaAtualUrl = '';
  currentYear = new Date().getFullYear();

  slug: string | null = null;
  previewId: string | null = null;

  private _timerInterval: any = null;
  private _contadores: Map<string, any> = new Map();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    private sanitizer: DomSanitizer,
    @Inject('BASE_URL') private baseUrl: string
  ) {}

  ngOnInit() {
    this.slug = this.route.snapshot.paramMap.get('slug');
    this.previewId = this.route.snapshot.paramMap.get('id');
    this.areaAtualUrl = this.route.snapshot.paramMap.get('area') ?? '';

    const url = this.slug
      ? `${this.baseUrl}site/slug/${this.slug}`
      : `${this.baseUrl}site/preview/${this.previewId}`;

    this.modo = this.slug ? 'slug' : 'preview';

    this.http.get<any>(url).subscribe({
      next: r => {
        this.site = r;
        this.carregando = false;
        if (!this.areaAtualUrl) {
          const areas: any[] = r.areas ?? [];
          const home = areas.find((a: any) => a.url?.toLowerCase() === 'home' && a.temLayout);
          const primeira = areas.find((a: any) => a.temLayout);
          this.areaAtualUrl = home?.url ?? primeira?.url ?? areas[0]?.url ?? '';
        }
      },
      error: e => {
        this.erro = e.status === 404 ? 'Site não encontrado.' : 'Erro ao carregar o site.';
        this.carregando = false;
      }
    });

    // Atualiza área ao navegar sem recarregar o componente
    this.route.paramMap.subscribe(params => {
      const a = params.get('area');
      if (a) this.areaAtualUrl = a;
    });
  }

  getAreaAtual(): any {
    const areas: any[] = this.site?.areas ?? [];
    if (!this.areaAtualUrl) return areas.find(a => a.temLayout) ?? null;
    return areas.find(a => a.url === this.areaAtualUrl)
        ?? areas.find(a => a.temLayout)
        ?? null;
  }

  getMenuNavegacao(): any[] {
    // Extrai todos os blocos menu-navegacao de todas as áreas (usado como global)
    for (const area of (this.site?.areas ?? [])) {
      const menu = (area.blocos ?? []).find((b: any) => b.tipo === 'menu-navegacao');
      if (menu) return [menu];
    }
    return [];
  }

  getBlocosConteudo(): any[] {
    return (this.getAreaAtual()?.blocos ?? []).filter((b: any) => b.tipo !== 'menu-navegacao');
  }

  navegarArea(url: string) {
    this.areaAtualUrl = url;
    if (this.slug) this.router.navigate(['/s', this.slug, url]);
    else if (this.previewId) this.router.navigate(['/preview', this.previewId, url]);
  }

  parseCampos(valor: string): any[] {
    try { return JSON.parse(valor) ?? []; } catch { return []; }
  }

  getVideoUrl(url: string): SafeResourceUrl {
    let embedUrl = url;
    // YouTube
    const ytMatch = url.match(/(?:youtube\.com\/watch\?v=|youtu\.be\/)([^&?/]+)/);
    if (ytMatch) embedUrl = `https://www.youtube.com/embed/${ytMatch[1]}`;
    // Vimeo
    const vimeoMatch = url.match(/vimeo\.com\/(\d+)/);
    if (vimeoMatch) embedUrl = `https://player.vimeo.com/video/${vimeoMatch[1]}`;
    return this.sanitizer.bypassSecurityTrustResourceUrl(embedUrl);
  }

  getContador(dataAlvo: string): any {
    if (!dataAlvo) return { encerrado: true };
    const key = dataAlvo;
    if (!this._contadores.has(key)) {
      this._contadores.set(key, this._calcContador(dataAlvo));
      if (!this._timerInterval) {
        this._timerInterval = setInterval(() => {
          this._contadores.forEach((_, k) => {
            this._contadores.set(k, this._calcContador(k));
          });
        }, 1000);
      }
    }
    return this._contadores.get(key);
  }

  private _calcContador(dataAlvo: string): any {
    const diff = new Date(dataAlvo).getTime() - Date.now();
    if (diff <= 0) return { encerrado: true };
    const s = Math.floor(diff / 1000);
    return {
      encerrado: false,
      dias: String(Math.floor(s / 86400)).padStart(2, '0'),
      horas: String(Math.floor((s % 86400) / 3600)).padStart(2, '0'),
      minutos: String(Math.floor((s % 3600) / 60)).padStart(2, '0'),
      segundos: String(s % 60).padStart(2, '0')
    };
  }

  ngOnDestroy() {
    if (this._timerInterval) clearInterval(this._timerInterval);
  }
}
