import { Observable } from "rxjs";

export interface IAuthSerice{
	baseUrl:string;
	register(...params:any[]):Observable<any>;
	login(...params:any[]):Observable<any>;
	isLoggedIn():boolean;
	setLogin(...params:any[]):any;
	logout(...params:any[]):any;
}
