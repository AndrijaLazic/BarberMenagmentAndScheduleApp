import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
	providedIn: 'root'
})
export class ChatService {

	baseUrl = environment.BACK_END_URL + "Workers/";

	constructor (private http: HttpClient) { }

	getWorkers (){
		return this.http.get(this.baseUrl+"Workers");
	}

	getWorkerChat (id:number){
		return this.http.request(
			"get", this.baseUrl+"WorkerChat?secondUserId="+id, {
				headers: {
					JWT: localStorage.getItem("JWT")!
				}
			}
		);
	}

}
