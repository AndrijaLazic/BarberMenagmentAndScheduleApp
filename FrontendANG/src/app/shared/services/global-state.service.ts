import { Injectable, WritableSignal, computed, signal } from '@angular/core';
import { IUser, Worker } from '../models/UserData';

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
		const work=this.userData() as Worker;
		if(work.lastName){
			return work.name!+" "+work.lastName;
		}
		return work.name;
	});

	getWorkerData (){
		return this.userData() as Worker;
	}

	constructor () {
	}

	setLogginState (state:boolean){
		this.loggedInState.set(state);
	}

	setUserData (data:IUser | null){
		this.userData.set(data);
	}

	setUnreadMessagesNumber (num:number){
		this.unreadMessagesNumber.set(num);
	}

	isWorker ():boolean{
		if((this.userData() as Worker).workerTypeId)
			return true;
		return false;
	}
}
