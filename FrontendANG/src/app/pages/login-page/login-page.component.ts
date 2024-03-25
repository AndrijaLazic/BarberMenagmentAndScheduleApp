import { CommonModule, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { NgToastService } from 'ng-angular-popup';
import { LoginDTS } from 'src/app/shared/models/LoginDTS';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
	selector: 'app-login-page',
	standalone: true,
	imports: [ 
		FormsModule,
		ReactiveFormsModule,
		CommonModule,
		NgIf 
	],
	templateUrl: './login-page.component.html',
	styleUrl: './login-page.component.css'
})
export class LoginPageComponent {

	myForm: FormGroup;
	loginDTS: LoginDTS = {
		email: "",
		password: ""
	};


	constructor (
    private formBuilder: FormBuilder, private service: AuthService, private router: Router, private toast: NgToastService
	){
		this.myForm = this.formBuilder.group({
			email: [ this.loginDTS.email, [ Validators.required, Validators.pattern('[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}') ] ],
			password: [ this.loginDTS.password, [ Validators.required, Validators.pattern('.{8,20}') ] ]
		});
	}


	showError (message: string) {
		this.toast.error({detail:"ERROR", summary: message, sticky:true});
	}

	showSuccess (message: string) {
		this.toast.success({detail:"SUCCESS", summary: message, duration: 5000});
	}

	


	onSubmit (){
		this.service.login(this.myForm.value).subscribe({
			error: () => {this.showError("Doslo je do greske.");},
			next: (response) => {
				console.log(response);
				this.showSuccess("Dobrodosli!");
				localStorage.setItem("JWT", response.data);
				localStorage.setItem("User", JSON.stringify(jwtDecode(response.data)));
				this.router.navigate([ "/" ]);
			}
		});
	}


}
