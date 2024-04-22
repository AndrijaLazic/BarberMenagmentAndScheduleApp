import { CommonModule, NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CurrentChat } from 'src/app/shared/models/signalR/CurrentChat';
import { ChatService } from 'src/app/shared/services/chat.service';
import { GlobalStateService } from 'src/app/shared/services/global-state.service';
import { SignalRService } from 'src/app/shared/services/signalR/signal-r.service';

@Component({
	selector: 'app-messaging-page',
	standalone: true,
	imports: [ NgFor, CommonModule, FormsModule ],
	templateUrl: './messaging-page.component.html',
	styleUrl: './messaging-page.component.css'
})
export class MessagingPageComponent implements OnInit{

	constructor (
		public signalR: SignalRService, private chatService: ChatService, public globalState:GlobalStateService
	){}

	currentChatId=-1;

	messageText="";

	ngOnInit (): void {
		this.signalR.startConnection(localStorage.getItem("JWT")!);

		this.chatService.getWorkers().subscribe((response:any)=>{
			this.signalR.workers.set(response.data);
			const userData=this.signalR.workers().find(x=> x.id==this.globalState.getWorkerData().id);
			if(userData){
				this.signalR.workers().splice(this.signalR.workers().indexOf(userData), 1);
			}
		});
	}

	showChat (id:number){
		this.chatService.getWorkerChat(id).subscribe((response:any)=>{
			const newChat=new CurrentChat();
			newChat.id=id;
			newChat.messages=response.data;
			this.signalR.currentChat.set(newChat);
			this.currentChatId=id;
			console.log(this.signalR.currentChat());
			this.signalR.JoinChatWithUser(+this.globalState.getWorkerData().id!, id)
				.then((x)=>{
					console.log(x);
				})
				.catch((error)=>{
					console.log(error);
				});
		});
	}


	sendMessage (){
		this.signalR.SendMessage(this.currentChatId, this.messageText).then(()=>{
			this.messageText="";
		});
	}
}
