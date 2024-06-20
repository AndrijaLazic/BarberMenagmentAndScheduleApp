import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IAuthSerice } from '../IAuthService';
import { Observable } from 'rxjs';
import { RegisterWorkerDTS } from '../../models/admin/RegisterWorkerDTS';
import { HttpClient } from '@angular/common/http';
import { GlobalStateService } from '../global-state.service';
import { LoginDTS } from '../../models/LoginDTS';
import { jwtDecode } from 'jwt-decode';

@Injectable({
	providedIn: 'root'
})
export class AdminAuthService implements IAuthSerice {
	baseUrl=environment.BACK_END_URL+"Workers/";
	constructor (private http: HttpClient, private globalState:GlobalStateService) {
		if(this.isLoggedIn()){
			globalState.setLogginState(true);
			globalState.setUserData(JSON.parse(localStorage.getItem("User")!));
			return;
		}

		globalState.setLogginState(false);
	}



	register (body: RegisterWorkerDTS):Observable<any> {
		return this.http.post(this.baseUrl+'Register', body);
	}

	login (body: LoginDTS): Observable<any>{
		return this.http.post(this.baseUrl+'Login', body);
	}

	isLoggedIn ():boolean{
		const jwt=localStorage.getItem("JWT");
		if(jwt==null){
			return false;
		}
		if(this.tokenExpired(jwt)){
			this.logout();
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

	tokenExpired (token: string) {
		const expiry = (JSON.parse(atob(token.split('.')[1]))).exp;
		return (Math.floor((new Date).getTime() / 1000)) >= expiry;
	}
}
