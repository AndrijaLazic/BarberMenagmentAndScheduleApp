import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterDTS } from '../models/RegisterDTS';
import { environment } from 'src/environments/environment';
import { LoginDTS } from '../models/LoginDTS';

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

	login (body: LoginDTS): Observable<any>{
		console.log(body);
		return this.http.post(this.baseUrl+'Login', body);
	}

	isLoggedIn ():boolean{

		if(localStorage.getItem("JWT")==null){
			return false;
		}

		return true;
	}

	logout (){
		localStorage.removeItem("JWT");
	}

}
