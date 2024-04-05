import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule, NgIf } from '@angular/common';
import { GlobalStateService } from '../../services/global-state.service';

@Component({
	selector: 'app-navigation-bar',
	standalone: true,
	imports: [ RouterLink, NgIf, CommonModule ],
	templateUrl: './navigation-bar.component.html',
	styleUrl: './navigation-bar.component.css'
})
export class NavigationBarComponent {


	constructor (
		private authService: AuthService,
		private router: Router,
		public globalState:GlobalStateService
	){}



	logout (){
		this.authService.logout();
		this.router.navigate([ "/prijava" ]);
	}

	messages (){
		this.router.navigate([ "/poruke" ]);
	}

}
