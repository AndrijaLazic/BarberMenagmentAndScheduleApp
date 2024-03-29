import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterDTS } from '../models/RegisterDTS';
import { environment } from 'src/environments/environment';
import { LoginDTS } from '../models/LoginDTS';
import { GlobalStateService } from './global-state.service';
import { jwtDecode } from 'jwt-decode';

@Injectable({
	providedIn: 'root'
})
export class AuthService {

	baseUrl=environment.BACK_END_URL+"Authentication/";

	constructor (private http: HttpClient, private globalState:GlobalStateService) {
		if(this.isLoggedIn()){
			globalState.setLogginState(true);
			globalState.setUserData(JSON.parse(localStorage.getItem("User")!));
			return;
		}

		globalState.setLogginState(false);
	}

	register (body: RegisterDTS):Observable<any> {
		return this.http.post(this.baseUrl+'Register', body);
	}

	login (body: LoginDTS): Observable<any>{
		return this.http.post(this.baseUrl+'Login', body);
	}

	isLoggedIn ():boolean{

		if(localStorage.getItem("JWT")==null){
			return false;
		}

		return true;
	}

	setLogin (JWT:string){
		localStorage.setItem("JWT", JWT);
		localStorage.setItem("User", JSON.stringify(jwtDecode(JWT)));
		this.globalState.setLogginState(true);
		this.globalState.setUserData(JSON.parse(localStorage.getItem("User")!));
	}

	logout (){
		localStorage.removeItem("JWT");
		localStorage.removeItem("User");
		this.globalState.setLogginState(false);
		this.globalState.setUserData(null);
	}

}
