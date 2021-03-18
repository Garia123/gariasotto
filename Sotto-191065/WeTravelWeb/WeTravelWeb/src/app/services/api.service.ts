import { HttpClient } from '@angular/common/http';// RXJS
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';// Modules
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class ApiService {
  constructor(private http: HttpClient) { }

  get<T>(url: string): Observable<T> {
    return this.http.get<T>(`${environment.apiUrl}/${url}`);
  }

  post<T>(url: string, body: any): Observable<T> {
    return this.http.post<T>(`${environment.apiUrl}/${url}`, body);
  }

  put<T>(url: string, body: any): Observable<T> {
    return this.http.put<T>(`${environment.apiUrl}/${url}`, body);
  }

  delete<T>(url: string): Observable<T> {
    return this.http.delete<T>(`${environment.apiUrl}/${url}`);
  }
}​​​​
