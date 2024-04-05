import { Injectable, WritableSignal, computed, signal } from '@angular/core';
import { IUser, User, Worker } from '../models/UserData';

@Injectable({
	providedIn: 'root'
})
export class GlobalStateService {
	loggedInState:WritableSignal<boolean>=signal(false);
	userData:WritableSignal<IUser | null>=signal(null);
	unreadMessagesNumber:WritableSignal<number>=signal(0);
	userName=computed(()=>{
		if(this.userData()==null)
			return "";
		if(this.userData() instanceof User){
			return (this.userData() as User).Name!;
		}
		const work=this.userData() as Worker;
		return work.Name!+" "+work.LastName;
	});

	constructor () {
	}

	setLogginState (state:boolean){
		this.loggedInState.set(state);
	}

	setUserData (data:IUser | null){
		console.log(data);
		this.userData.set(data);
	}

	setUnreadMessagesNumber (num:number){
		this.unreadMessagesNumber.set(num);
	}

	isWorker ():boolean{
		return this.userData() instanceof Worker;
	}
}
