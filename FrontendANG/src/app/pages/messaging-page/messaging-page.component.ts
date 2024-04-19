import { CommonModule, NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Worker } from 'src/app/shared/models/UserData';
import { ChatService } from 'src/app/shared/services/chat.service';
import { GlobalStateService } from 'src/app/shared/services/global-state.service';
import { SignalRService } from 'src/app/shared/services/signalR/signal-r.service';

@Component({
	selector: 'app-messaging-page',
	standalone: true,
	imports: [ NgFor, CommonModule ],
	templateUrl: './messaging-page.component.html',
	styleUrl: './messaging-page.component.css'
})
export class MessagingPageComponent implements OnInit{

	constructor (
		private signalR: SignalRService, private chatService: ChatService, private globalState:GlobalStateService
	){}

	workers: Worker[] = [];

	currentChat: any;

	ngOnInit (): void {
		this.signalR.startConnection(localStorage.getItem("JWT")!);

		this.chatService.getWorkers().subscribe((response:any)=>{
			this.workers = response.data;
			const userData=this.workers.find(x=> x.id==this.globalState.getWorkerData().id);
			if(userData){
				this.workers.splice(this.workers.indexOf(userData), 1);
			}
		});
	}

	showChat (id:number){
		this.chatService.getWorkerChat(id).subscribe((response:any)=>{
			this.currentChat = response.data;
			console.log(this.currentChat);
		});
	}


}
