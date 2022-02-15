import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError, take } from 'rxjs/operators';

import { AccountService } from '@app/services/account.service';
import { User } from '@app/models/identity/user';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser: User;

    this.accountService.currentUse$.pipe(take(1)).subscribe(user => {
      currentUser = user;

      if (currentUser) {
        request = request.clone({
          setHeaders: {
            Authorization: `Bearer ${currentUser.token}`
          }
        });
      }
    });

    return next.handle(request).pipe(
      catchError(error => {
        if (error) {
          localStorage.removeItem('user');
        }
        return throwError(error);
      })
    );
  }
}
