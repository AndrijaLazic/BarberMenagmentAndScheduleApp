import { CommonModule, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
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
private formBuilder: FormBuilder, private service: AuthService, private router: Router
	) {
		this.myForm = this.formBuilder.group({
			name:  [ this.registerdts.name, [ Validators.required, Validators.pattern('.{8,20}') ] ],
			email: [ this.registerdts.email, [ Validators.required, Validators.pattern('[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}') ] ],
			password: [ this.registerdts.password, [ Validators.required, Validators.pattern('.{8,20}') ] ],
			phoneNumber: [ this.registerdts.phoneNumber, [ Validators.required, Validators.pattern('\\d{10}') ] ]
		});
	}



	onSubmit (){
		this.service.register(this.myForm.value).subscribe({
			error: () => {alert("Doslo je do greske.");},
			complete: () => {
				alert("Uspesna registracija.");
				this.router.navigate([ "/" ]);
			}
		});
	}
}
