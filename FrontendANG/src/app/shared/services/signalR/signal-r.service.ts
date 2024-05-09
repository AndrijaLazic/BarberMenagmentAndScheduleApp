import { Injectable, WritableSignal, signal } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { environment } from 'src/environments/environment';
import { GlobalStateService } from '../global-state.service';
import { NgToastService } from 'ng-angular-popup';
import { Worker } from '../../models/UserData';
import { CurrentChat } from '../../models/signalR/CurrentChat';


@Injectable({
	providedIn: 'root'
})
export class SignalRService {

	private hubConnection!: signalR.HubConnection;

	constructor (public globalService:GlobalStateService, private toast: NgToastService) { }

	workers:WritableSignal<Worker[]>=signal([]);
	currentChat:WritableSignal<CurrentChat | null>=signal(null);

	public startConnection = async (JWT:string) => {
		this.hubConnection = new signalR.HubConnectionBuilder()
			.withUrl(environment.SOCKET_URL, {
				skipNegotiation: true,
				transport: signalR.HttpTransportType.WebSockets,
				withCredentials: false
			})
			.configureLogging(signalR.LogLevel.Information)
			.build();

		return this.hubConnection.start()
			.then(async () => {
				console.log('Connection started');
				this.setSignalrClientMethods();
				this.JoinServer(JWT).catch(err=> {
					console.log(err);
				});
				return true;
			})
			.catch(err => {
				console.log('Error while starting connection: ' + err);
				return false;
			});
	};

	private setSignalrClientMethods (): void {
		this.hubConnection.on('JoinedMessage', (message:any)=>{
			console.log(message);
		});
		this.hubConnection.on('ReceiveSpecificMessage', (sender:any, message:any)=>{
			console.log(sender+" "+this.currentChat()?.user?.id);
			if(sender==this.currentChat()?.user?.id || sender==this.globalService.getWorkerData().id){
				this.currentChat.update(currentValue=>{
					currentValue?.messages.push({
						id:-1,
						message:message,
						senderId:sender,
						communicationId:currentValue.user!.id!
					});
					return currentValue;
				});
				return;
			}

			this.globalService.setUnreadMessagesNumber(this.globalService.unreadMessagesNumber()+1);
			this.toast.info({detail:"Imate "+this.globalService.unreadMessagesNumber()+" novih poruka", duration:2000});
		});
		this.hubConnection.on('ValidationError', (message:any)=>{
			console.log(message);
			this.toast.error({detail:message, duration:2000});
		});
		this.hubConnection.on("JoinedServerMessage", (message:any)=>{
			const userWithID=this.workers().find((x)=>x.id==message);
			if(userWithID){
				userWithID.isOnline=true;
			}
		});
		this.hubConnection.on("DisconnectedFromAppMessage", (message:any)=>{
			const userWithID=this.workers().find((x)=>x.id==message);
			if(userWithID){
				userWithID.isOnline=false;
			}
		});


	}

	public async JoinServer (JWT:string){
		return this.hubConnection.invoke("JoinServer", JWT);
	}

	public JoinChatWithUser = async (userId1: number, userId2: number) => {
		return this.hubConnection.invoke(
			'JoinChatWithUser', userId1, userId2
		);
	};

	public SendMessage = async (userId2: number, message:string) => {
		return this.hubConnection.invoke(
			'SendMessage', userId2, message
		);
	};

}
