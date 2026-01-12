import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isAuthenticated: boolean = false;
  private http = inject(HttpClient);
  private baseUrl: string = 'http://localhost:5000/api/identity/auth';

  login() {
    this.isAuthenticated = true;
  }

  logout() {
    this.isAuthenticated = false;
  }

  isLoggedIn(): boolean {
    return this.isAuthenticated;
  }

  register(data: any) {
    return this.http.post<any>(`${this.baseUrl}/register`, data);
  }
}
