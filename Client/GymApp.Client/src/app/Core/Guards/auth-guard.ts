import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../Services/auth-service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  let isAuthenticated = false;
  let isAdmin = false;

  authService.authState$.subscribe(res => {
    isAuthenticated = res.isAuthenticated
    isAdmin = res.isAdmin;
  });
  
  if (isAdmin) {
    return true;
  } else {
    if (isAuthenticated) {
      router.navigate(['/']);  
    } else {
      router.navigate(['/identity/login']);
    }
    return false;
  }
};