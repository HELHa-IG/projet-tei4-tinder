import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {Result} from "../models/result";
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class ResultService {
  private apiUrl = 'https://localhost:7150/api/Results';

  constructor(private http: HttpClient, private cookieService:CookieService) { }

  insertResult(result: Result): Observable<any> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post<Result>(`${this.apiUrl}`, result, { headers: headers });
  }

  getResults(userId: number): Observable<any> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.apiUrl}/byUserId/${userId}`, { headers: headers });
  }
}
