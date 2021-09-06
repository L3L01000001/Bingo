import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(err => {
        if(err){

          if(err.status == 400){
            //validation error
            if(err.error.errors){
              throw(err.error);
            }
            //bad request
            else{
              this.toastr.error(err.error.message, err.error.statusCode);
              
            }
          }
          
          if(err.status == 401){
            this.toastr.error(err.error.message, err.error.statusCode);
          }

          if(err.status === 404){
            this.router.navigateByUrl('/not-found');
          }
          if(err.status === 500){
            //slanje errora preko redirekcije
            const navigationExtras: NavigationExtras = {state: {error: err.error}}
            this.router.navigateByUrl('/server-error', navigationExtras);
          }
        }
        return throwError(err);
      })
    )
  }
}
