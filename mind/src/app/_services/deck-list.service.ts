import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Deck } from '../models/deck';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class DeckListService {

  private baseUrl = 'https://localhost:7150/api/Decks';

  constructor(private http: HttpClient, private cookieService:CookieService) { }

  getDecks(userID: number): Observable<any> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}/User/${userID}`, { headers: headers });
  }

  getDeck(id: number): Observable<any> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}/${id}`, { headers: headers });
  }

  createDeck(deck: Deck): Observable<Deck> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post<Deck>(`${this.baseUrl}`, deck, { headers: headers });
  }

  updateDeck(id: number, value: Deck): Observable<Deck> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.put<Deck>(`${this.baseUrl}/${id}`, value, { headers: headers });
  }

  deleteDeck(id: number): Observable<any> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.baseUrl}/${id}`, { headers: headers, responseType: 'text' });
  }

  deleteAllDecks(): Observable<any> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.delete(`${this.baseUrl}` + `/delete`, { headers: headers, responseType: 'text' });
  }

  getDecksList(): Observable<any> {
    let token = this.cookieService.get('token');
    console.log(token);
    let headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get(`${this.baseUrl}`, { headers: headers });
  }
}
