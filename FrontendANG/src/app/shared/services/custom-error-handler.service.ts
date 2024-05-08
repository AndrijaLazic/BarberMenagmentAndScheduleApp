/* eslint-disable indent */
import { ErrorHandler, Injectable, NgZone } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';

@Injectable()
export class CustomErrorHandlerService implements ErrorHandler {

	constructor (private toast: NgToastService, private zone:NgZone) { }
	handleError (error: any): void {
		this.zone.run(()=>{
			this.toast.error({detail:"ERROR", summary: "Error was detected", duration: 3000});
		});

		console.warn(error);
	}
}
