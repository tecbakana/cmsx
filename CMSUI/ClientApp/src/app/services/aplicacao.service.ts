import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()

export class AplicacaoService {
  myAppUrl: string = "";
  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  getAppList() {
    return this._http.get(this.myAppUrl + 'Aplicacaos');
  }

  errorHandler(error: Response) {
    console.log(error);
    return throwError(()=>new Error(error.text.toString()));
  }
}
