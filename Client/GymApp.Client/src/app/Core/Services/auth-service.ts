import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private authStateSubject = new BehaviorSubject<{ isAuthenticated: boolean; userEmail: string | null; }>(
  {
    isAuthenticated: localStorage.getItem('isAuthenticated') === 'true',
    userEmail: localStorage.getItem('userEmail')
  });
  public authState$ = this.authStateSubject.asObservable();
  
  private http = inject(HttpClient);
  private baseUrl: string = 'http://localhost:5000/api/identity/auth';

  login(userEmail: string) {
    localStorage.setItem('isAuthenticated', 'true');
    localStorage.setItem('userEmail', userEmail);
    this.authStateSubject.next({ isAuthenticated: true, userEmail: userEmail });
  }

  logout() {
    localStorage.removeItem('isAuthenticated');
    localStorage.removeItem('userEmail');
    this.authStateSubject.next({ isAuthenticated: false, userEmail: null });
  }

  isLoggedIn(): boolean {
    return this.authStateSubject.value.isAuthenticated;
  }

  register(data: any) {
    return this.http.post<any>(`${this.baseUrl}/register`, data);
  }
}
