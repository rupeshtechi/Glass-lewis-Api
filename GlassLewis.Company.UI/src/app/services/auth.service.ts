import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private isAuthenticatedSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(!!localStorage.getItem('token'));
  public isAuthenticated$: Observable<boolean> = this.isAuthenticatedSubject.asObservable();

  
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/user/login`, { username, password });
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  setLogin(token : string ) {
    localStorage.setItem('token', token);
    this.isAuthenticatedSubject.next(true);
  }

  logout(): void {
    localStorage.removeItem('token');
    this.isAuthenticatedSubject.next(false);
  }
}
