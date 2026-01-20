import { HttpHandlerFn, HttpInterceptorFn, HttpRequest, HttpResponse } from "@angular/common/http";
import { tap } from "rxjs";

export const AuthInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => {
    let token = localStorage.getItem('access-token');

    if (token) {   
        req = req.clone({
            setHeaders: {
                'Authorization': `Bearer ${token}`
            }
        })
    }

    return next(req).pipe(
        tap(event1 => {
            if (event1 instanceof HttpResponse) {
                console.log('Inserted token into request');
            }
        })
    );
};