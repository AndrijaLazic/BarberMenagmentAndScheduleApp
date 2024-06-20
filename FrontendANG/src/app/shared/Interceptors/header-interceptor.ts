/* eslint-disable indent */
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
	providedIn:"root"
})
export class globalHttpHeaderInterceptor implements HttpInterceptor{

	intercept (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		const JWT=localStorage.getItem("JWT");
		//before request
		// eslint-disable-next-line function-paren-newline
		if(JWT==null){
			console.warn("No JWT provided");
			return next.handle(req);
		}
		console.warn("Dodajem token");
		req = req.clone({
			headers: req.headers.set("JWT", JWT)
			});

		return next.handle(req);
	}

}
