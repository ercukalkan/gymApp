import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private baseUrl: string = 'http://localhost:5000/api/identity/auth';

  private authStateSubject = new BehaviorSubject<{ isAuthenticated: boolean, email: string | null, isAdmin: boolean }>
  ({
    isAuthenticated: !!localStorage.getItem('access-token'),
    email: localStorage.getItem('email') || null,
    isAdmin: Boolean(localStorage.getItem('isAdmin')) || false
  });
  public authState$ = this.authStateSubject.asObservable();

  login(data: any) {
    return this.http.post<any>(`${this.baseUrl}/login`, data)
      .pipe(
        tap(res => {
          localStorage.setItem('access-token', res.accessToken);
          localStorage.setItem('refresh-token', res.refreshToken);
          localStorage.setItem('email', res.user.email);
          localStorage.setItem('isAdmin', String(res.user.isAdmin));
        }),
        tap(res => {
          this.authStateSubject.next({
            isAuthenticated: res.success,
            email: res.user.email,
            isAdmin: res.user.isAdmin
          });
        }),
        catchError(err => {
          console.error('Login failed: ', err);
          return throwError(() => err);
        })
      );
  }

  logout() {
    this.clearLocalStorage();

    return this.http.post<any>(`${this.baseUrl}/logout`, null).pipe(
      tap(() => {
        this.authStateSubject.next({
          isAuthenticated: false,
          email: null,
          isAdmin: false
        });
      }),
      catchError(err => {
        console.error('Logout failed: ', err);
        return throwError(() => err);
      })
    );
  }

  register(data: any) {
    return this.http.post<any>(`${this.baseUrl}/register`, data);
  }

  getUsers() {
    return this.http.get<any>(`${this.baseUrl}`);
  }

  getUser(userId: any) {
    return this.http.get<any>(`${this.baseUrl}/${userId}`);
  }

  updateUser(userId: any, user: any) {
    return this.http.put(`${this.baseUrl}/${userId}`, user);
  }

  deleteUser(userId: any) {
    return this.http.delete<any>(`${this.baseUrl}/${userId}`);
  }

  getRoles() {
    return this.http.get<any>(`${this.baseUrl}/roles`);
  }

  private clearLocalStorage() {
    localStorage.removeItem('access-token');
    localStorage.removeItem('refresh-token');
    localStorage.removeItem('email');
    localStorage.removeItem('isAdmin');
    this.authStateSubject.next({
      isAuthenticated: false,
      email: null,
      isAdmin: false
    });
  }
}
