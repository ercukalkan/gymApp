import { HttpInterceptorFn, HttpRequest, HttpHandlerFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const ErrorInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn,
) => {
  return next(req).pipe(
    catchError((error) => {
      if (error.status === 401) {
        console.error('Unauthorized request - 401');
        window.location.replace('/identity/login');
      }
      if (error.status === 403) {
        console.error('Forbidden request - 403');
        window.location.replace('/forbidden');
      }
      if (error.status === 500) {
        console.error('Server error - 500');
        window.location.replace('/server-error');
      }
      if (error.status === 404) {
        console.error('Not found - 404');
        window.location.replace('/not-found');
      }
      if (error.status === 400) {
        console.error('Bad request - 400');
        if (error.error && error.error.errors) {
          const validationErrors = [];
          for (const key in error.error.errors) {
            if (error.error.errors[key]) {
              validationErrors.push(error.error.errors[key]);
            }
          }
          throw validationErrors.flat();
        }
      }
      return throwError(() => error);
    }),
  );
};
