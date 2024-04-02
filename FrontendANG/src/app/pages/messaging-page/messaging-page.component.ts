import { CommonModule, NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ChatService } from 'src/app/shared/services/chat.service';
import { SignalRService } from 'src/app/shared/services/signal-r.service';

@Component({
	selector: 'app-messaging-page',
	standalone: true,
	imports: [ NgFor, CommonModule ],
	templateUrl: './messaging-page.component.html',
	styleUrl: './messaging-page.component.css'
})
export class MessagingPageComponent implements OnInit{

	constructor (private signalR: SignalRService, private chatService: ChatService){}

	workers: any[] = [];

	currentChat: any;

	ngOnInit (): void {
		this.signalR.startConnection(localStorage.getItem("JWT")!);

		this.chatService.getWorkers().subscribe((response:any)=>{
			this.workers = response.data;
		});
		
	}

	showChat (id:number){
		this.chatService.getWorkerChat(id).subscribe((response:any)=>{
			this.currentChat = response.data;
			console.log(this.currentChat);
		});
	}


}
