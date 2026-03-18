import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export interface TenantCtx { aplicacaoid: string; nome: string; }

@Injectable({ providedIn: 'root' })
export class AdminContextService {
  private _tenant = new BehaviorSubject<TenantCtx | null>(this.loadStored());
  tenant$ = this._tenant.asObservable();

  get tenantId(): string | null { return this._tenant.value?.aplicacaoid ?? null; }

  set(tenant: TenantCtx | null) {
    this._tenant.next(tenant);
    if (tenant) sessionStorage.setItem('adminTenant', JSON.stringify(tenant));
    else sessionStorage.removeItem('adminTenant');
  }

  private loadStored(): TenantCtx | null {
    try { return JSON.parse(sessionStorage.getItem('adminTenant') ?? 'null'); }
    catch { return null; }
  }
}
