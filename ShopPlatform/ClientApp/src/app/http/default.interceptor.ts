import {Injectable, Injector} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {AuthenticationService} from '../authentication/authentication.service';

@Injectable()
export class DefaultInterceptor implements HttpInterceptor{
  constructor(private injector: Injector, private authService: AuthenticationService) {
  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!req.url.includes('api/')){
      return next.handle(req);
    }
    if (!req.headers.has('Content-Type') && !req.url.includes('api/shop/uploadicon')){
      req = req.clone({
        headers: req.headers.set('Content-Type', 'application/json')
      });
    }
    if (!req.headers.has('Authorization')){
      const accessToken = this.authService.LoggedUser?.accessToken;
      if (accessToken != null) {
        req = req.clone({
          headers: req.headers.set('Authorization', `Bearer ${accessToken}`)
        });
      }
    }
    return next.handle(req).pipe(catchError((error: HttpErrorResponse) => {
      return throwError(error);
    }));
  }
}
