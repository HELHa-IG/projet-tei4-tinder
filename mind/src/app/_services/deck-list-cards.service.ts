import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Card } from '../models/card';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class DeckListCardsService {

  private baseUrl = 'https://localhost:7150/api/Cards';

  constructor(private http: HttpClient, private cookieService:CookieService) { }

  getCards(deckID: number): Observable<any> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}/Deck/${deckID}`, { headers: headers });
  }

  getCard(id: number): Observable<any> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}/${id}`, { headers: headers });
  }

  createCard(card: Card): Observable<Card> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post<Card>(`${this.baseUrl}`, card, { headers: headers });
  }

  updateCard(id: number, card: Card): Observable<Card> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.put<Card>(`${this.baseUrl}/${id}`, card, { headers: headers });
  }

  deleteCard(id: number): Observable<any> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.baseUrl}/${id}`, { headers: headers, responseType: 'text' });
  }
}
