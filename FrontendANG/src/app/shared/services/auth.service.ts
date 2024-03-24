import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterDTS } from '../models/RegisterDTS';

@Injectable({
	providedIn: 'root'
})
export class AuthService {
	
	constructor (private http: HttpClient) {}

	register (body: RegisterDTS):Observable<any> {
		console.log(body);
		return this.http.post('https://localhost:7278/Authentication/Register', body);
	}
	
}
