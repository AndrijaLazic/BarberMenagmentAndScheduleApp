/* eslint-disable indent */
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
	providedIn:"root"
})
export class globalHttpErrorHandlerInterceptor implements HttpInterceptor{
	intercept (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

		//before request
		// eslint-disable-next-line function-paren-newline
		return next.handle(req).pipe(
			//after
			catchError(err=>{
				console.log("Error handled by HttpInterceptor", err);
				return throwError(()=>{
					return err;
				});
			}));
	}

}
