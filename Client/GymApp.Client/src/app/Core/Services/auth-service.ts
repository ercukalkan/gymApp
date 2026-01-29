import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { JWTPayload } from '../Interfaces/JWTPayload';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private baseUrl: string = 'http://localhost:5000/api/identity/auth';

  private authStateSubject = new BehaviorSubject<{
    userId: string | null,
    isAuthenticated: boolean,
    firstName: string | null,
    lastName: string | null,
    isAdmin: boolean
  }>
  ({
    userId: this.getUserIdFromToken(),
    isAuthenticated: !!localStorage.getItem('access-token'),
    firstName: localStorage.getItem('firstName'),
    lastName: localStorage.getItem('lastName'),
    isAdmin: localStorage.getItem('isAdmin') === 'true'
  });
  public authState$ = this.authStateSubject.asObservable();

  login(data: any) {
    return this.http.post<any>(`${this.baseUrl}/login`, data)
      .pipe(
        tap(res => {
          this.updateLocalStorageTokens(res.accessToken, res.refreshToken);
          this.updateLocalStorageNames(res.user.firstName, res.user.lastName, res.user.isAdmin);
        }),
        tap(res => {
          this.authStateSubject.next({
            userId: res.user.id,
            isAuthenticated: res.success,
            firstName: res.user.firstName,
            lastName: res.user.lastName,
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
    return this.http.post<any>(`${this.baseUrl}/logout`, null).pipe(
      tap(() => {
        this.authStateSubject.next({
          userId: '',
          isAuthenticated: false,
          firstName: null,
          lastName: null,
          isAdmin: false
        });

        this.clearLocalStorage();
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
    return this.http.put<any>(`${this.baseUrl}/${userId}`, user)
      .pipe(
        tap(res => {
          if (this.getUserIdFromToken() === userId) {
            localStorage.setItem('firstName', res.firstName);
            localStorage.setItem('lastName', res.lastName);
          }
        })
      );
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
    localStorage.removeItem('isAdmin');
    localStorage.removeItem('firstName');
    localStorage.removeItem('lastName');
    this.authStateSubject.next({
      userId: null,
      isAuthenticated: false,
      firstName: null,
      lastName: null,
      isAdmin: false
    });
  }

  private updateLocalStorageNames(firstName: string, lastName: string, isAdmin: boolean) {
    localStorage.setItem('firstName', firstName);
    localStorage.setItem('lastName', lastName);
    localStorage.setItem('isAdmin', String(isAdmin));
  }

  private updateLocalStorageTokens(accessToken: string, refreshToken: string) {
    localStorage.setItem('access-token', accessToken);
    localStorage.setItem('refresh-token', refreshToken);
  }

  private getUserIdFromToken(): string {
    const token = localStorage.getItem('access-token');
    if (!token) return '';
    const decoded = jwtDecode<JWTPayload>(token);
    return decoded.userId;
  }
}
