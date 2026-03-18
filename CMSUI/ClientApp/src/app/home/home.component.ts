import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  stats: any = null;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  ngOnInit() {
    this.http.get<any>(this.baseUrl + 'dashboard').subscribe(s => this.stats = s);
  }
}
