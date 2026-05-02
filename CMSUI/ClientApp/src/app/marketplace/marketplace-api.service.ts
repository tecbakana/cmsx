import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class MarketplaceApiService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  getPedidos(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'marketplace/pedidos');
  }

  getPedido(id: string): Observable<any> {
    return this.http.get<any>(this.baseUrl + `marketplace/pedidos/${id}`);
  }

  emitirNf(id: string): Observable<any> {
    return this.http.post<any>(this.baseUrl + `marketplace/pedidos/${id}/emitir-nf`, {});
  }
}
