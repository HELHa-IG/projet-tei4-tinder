import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Card } from '../models/card';
import { CookieService } from 'ngx-cookie-service';


@Injectable({
  providedIn: 'root'
})
export class CardUploadService {
  private apiUrl = 'https://localhost:7150/api/CardUploadModels';

  constructor(private http: HttpClient, private cookieService:CookieService) { }

  uploadCard(formData: FormData): Observable<any> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    headers = headers.set('accept', 'text/plain');
    return this.http.post(this.apiUrl, formData, { headers: headers });
  }

  deleteCard(cardId: number): Observable<any> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.apiUrl}/${cardId}`, { headers: headers});
  }
}
