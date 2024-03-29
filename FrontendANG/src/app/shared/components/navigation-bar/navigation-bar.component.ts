import { Component, WritableSignal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { NgIf } from '@angular/common';
import { GlobalStateService } from '../../services/global-state.service';
import { UserData } from '../../models/UserData';

@Component({
	selector: 'app-navigation-bar',
	standalone: true,
	imports: [ RouterLink, NgIf ],
	templateUrl: './navigation-bar.component.html',
	styleUrl: './navigation-bar.component.css'
})
export class NavigationBarComponent {



	constructor (
		private authService: AuthService,
		private router: Router,
		private globalState:GlobalStateService
	){}

	isLoggedIn: WritableSignal<boolean> = this.globalState.loggedInState;
	userData: WritableSignal<UserData | null> = this.globalState.userData;

	logout (){
		this.authService.logout();
		this.router.navigate([ "/prijava" ]);
	}

}
