import { CommonModule, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { RegisterDTS } from 'src/app/shared/models/RegisterDTS';
import { AuthService } from 'src/app/shared/services/auth.service';


@Component({
	selector: 'app-register-page',
	standalone: true,
	imports: [ 
		FormsModule,
		ReactiveFormsModule,
		CommonModule,
		NgIf
	],
	templateUrl: './register-page.component.html',
	styleUrl: './register-page.component.css'
})
export class RegisterPageComponent{

	myForm: FormGroup;
	registerdts: RegisterDTS = {
		email: "",
		name: "",
		password: "",
		phoneNumber: ""
	};

	constructor (
private formBuilder: FormBuilder, private service: AuthService, private router: Router, private toast: NgToastService
	) {
		this.myForm = this.formBuilder.group({
			name:  [ this.registerdts.name, [ Validators.required, Validators.pattern('.{8,20}') ] ],
			email: [ this.registerdts.email, [ Validators.required, Validators.pattern('[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}') ] ],
			password: [ this.registerdts.password, [ Validators.required, Validators.pattern('.{8,20}') ] ],
			phoneNumber: [ this.registerdts.phoneNumber, [ Validators.required, Validators.pattern('\\d{10}') ] ]
		});
	}
	showError (message: string) {
		this.toast.error({detail:"ERROR", summary: message, sticky:true});
	}

	showSuccess (message: string) {
		this.toast.success({detail:"SUCCESS", summary: message, duration: 5000});
	}


	onSubmit (){
		this.service.register(this.myForm.value).subscribe({
			error: () => {this.showError("Doslo je do greske.");},
			complete: () => {
				this.showSuccess("Uspesna registracija!");
				this.router.navigate([ "/" ]);
			}
		});
	}
}
