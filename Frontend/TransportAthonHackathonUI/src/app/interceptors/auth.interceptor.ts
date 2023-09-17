import { Injectable, inject } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, mergeMap, tap, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private readonly authService = inject(AuthService)
  private readonly router = inject(Router)
  constructor() { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = localStorage.getItem('accessToken')
    const newRequest = request.clone({ headers: request.headers.set('Authorization', `Bearer ${token}`) });
    return next.handle(newRequest).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          return this.authService.refreshToken().pipe(
            catchError((error: HttpErrorResponse) => {
              this.router.navigateByUrl('/login')
              return throwError(() => error)
            }),
            tap(tokenResult => {
              this.authService.saveTokenResult(tokenResult);
            }),
            mergeMap(tokenResult => {
              const newRequest = request.clone({ headers: request.headers.set('Authorization', `Bearer ${tokenResult.token}`) });
              return next.handle(newRequest)
            })
          )
        }
        return throwError(() => error);
      })
    );
  }
}
