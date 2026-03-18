import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const raw = sessionStorage.getItem('usuario');
    if (raw) {
      const token = JSON.parse(raw).token;
      if (token) {
        return next.handle(req.clone({ setHeaders: { Authorization: `Bearer ${token}` } }));
      }
    }
    return next.handle(req);
  }
}
