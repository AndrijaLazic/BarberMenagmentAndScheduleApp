import { Injectable, WritableSignal, signal } from '@angular/core';
import { UserData } from '../models/UserData';

@Injectable({
	providedIn: 'root'
})
export class GlobalStateService {
	loggedInState:WritableSignal<boolean>=signal(false);
	userData:WritableSignal<UserData | null>=signal(null);

	constructor () {
	}

	setLogginState (state:boolean){
		this.loggedInState.set(state);
	}

	setUserData (data:UserData | null){
		this.userData.set(data);
	}
}
