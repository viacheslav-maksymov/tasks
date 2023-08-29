import { Injectable } from '@angular/core';
import {
  HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private router: Router, private cookieService: CookieService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (req.url.endsWith('/authenticate')) {
      return next.handle(req);
    }

    const authToken = this.cookieService.get('auth_token');

    const authReq = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${authToken}`)
    });

    return next.handle(authReq).pipe(
      catchError((error: HttpErrorResponse) => {

        if (error.status === 401) {
          this.router.navigate(['/user/login']);
          this.cookieService.delete('auth_token')
        }

        if (error.status === 403) {
          this.router.navigate(['/403']);
        }

        if (error.status === 404) {
          this.router.navigate(['/404']);
        }

        // Возвращаем Observable с ошибкой
        return throwError(error);
      })
    );
  }
}
