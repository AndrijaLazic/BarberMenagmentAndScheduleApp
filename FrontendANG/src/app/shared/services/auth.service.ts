import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterDTS } from '../models/RegisterDTS';
import { environment } from 'src/environments/environment';

@Injectable({
	providedIn: 'root'
})
export class AuthService {

	baseUrl=environment.BACK_END_URL+"Authentication/";

	constructor (private http: HttpClient) {}

	register (body: RegisterDTS):Observable<any> {
		console.log(body);
		return this.http.post(this.baseUrl+'Register', body);
	}

}
