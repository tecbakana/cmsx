import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()

export class UsuarioService {
  myAppUrl: string = "";
  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  getUserList() {
    return this._http.get(this.myAppUrl + 'Usuarios');
  }

  errorHandler(error: Response) {
    console.log(error);
    return throwError(()=>new Error(error.text.toString()));
  }
}
