import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getCompanyData(): Observable<any> {
    const token = localStorage.getItem('token');
    return this.http.get(`${this.apiUrl}/companies`, {
      headers: { Authorization: `Bearer ${token}` }
    });
  }
}
