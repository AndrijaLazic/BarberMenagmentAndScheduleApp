/* eslint-disable indent */
import { CommonModule, NgIf } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { LoginDTS } from 'src/app/shared/models/LoginDTS';
import { IAuthSerice } from 'src/app/shared/services/IAuthService';

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
		private formBuilder: FormBuilder,
		@Inject('IAuthService') private service: IAuthSerice,
		private router: Router,
		private toast: NgToastService
	){
		this.myForm = this.formBuilder.group({
			email: [ this.loginDTS.email, [ Validators.required, Validators.pattern('[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}') ] ],
			password: [ this.loginDTS.password, [ Validators.required, Validators.pattern('.{3,20}') ] ]
		});
	}


	showError (message: string) {
		this.toast.error({detail:"ERROR", summary: message, duration: 3000});
	}

	showSuccess (message: string) {
		this.toast.success({detail:"SUCCESS", summary: message, duration: 3000});
	}




	onSubmit (){
		this.service.login(this.myForm.value).subscribe({
			error: (error) => {
				const errorObject=error.error;
				switch(errorObject.message){
					case "EmailDoesNotExist": {
						this.showError("Dati email ne postoji");
						break;
					}
					case "PasswordNotValid": {
						this.showError("Sifra nije validna");
						break;
					}
					default: {
						throw new Error(error);
					}
				}
			},
			next: async (response) => {
				this.showSuccess("Dobrodosli!");
				this.service.setLogin(response.data);
				this.router.navigate([ "/" ]);
			}
		});
	}


}
