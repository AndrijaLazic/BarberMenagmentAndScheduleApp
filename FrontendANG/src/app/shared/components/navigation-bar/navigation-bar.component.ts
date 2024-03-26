import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { NgIf } from '@angular/common';

@Component({
	selector: 'app-navigation-bar',
	standalone: true,
	imports: [ RouterLink, NgIf ],
	templateUrl: './navigation-bar.component.html',
	styleUrl: './navigation-bar.component.css'
})
export class NavigationBarComponent implements OnInit {

	isLoggedIn: boolean = false;

	constructor (private authService: AuthService, private router: Router){}

	ngOnInit (): void {
		this.isLoggedIn = this.authService.isLoggedIn();
	}

	logout (){
		this.authService.logout();
		this.router.navigate([ "/prijava" ]).then(() => {
			window.location.reload();
		});
	}

}
