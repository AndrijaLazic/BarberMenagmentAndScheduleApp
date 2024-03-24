import { CommonModule, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


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
export class RegisterPageComponent implements OnInit {

	myForm: FormGroup;

	constructor (private formBuilder: FormBuilder) {
		this.myForm = this.formBuilder.group({
			name:  [ '', [ Validators.required, Validators.pattern('.{8,20}') ] ],
			email: [ '', [ Validators.required, Validators.pattern('[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}') ] ],
			password: [ '', [ Validators.required, Validators.pattern('.{8,20}') ] ],
			phoneNumber: [ '', [ Validators.required, Validators.pattern('\\d{10}') ] ]
		});
	}

	ngOnInit (): void {
		throw new Error('Method not implemented.');
	}

	onSubmit (){
		
	}
}
