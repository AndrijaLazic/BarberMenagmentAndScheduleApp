import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { environment } from 'src/environments/environment';
import { GlobalStateService } from '../global-state.service';
import { NgToastService } from 'ng-angular-popup';


@Injectable({
	providedIn: 'root'
})
export class SignalRService {

	private hubConnection!: signalR.HubConnection;

	constructor (public globalService:GlobalStateService, private toast: NgToastService) { }

	public startConnection = async (JWT:string) => {
		this.hubConnection = new signalR.HubConnectionBuilder()
			.withUrl(environment.SOCKET_URL, {
				skipNegotiation: true,
				transport: signalR.HttpTransportType.WebSockets,
				withCredentials: false,
				headers:{"JWT":JWT}
			})
			.configureLogging(signalR.LogLevel.Information)
			.build();

		return this.hubConnection.start()
			.then(() => {
				console.log('Connection started');
				this.setSignalrClientMethods();
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
			this.toast.success(message);
		});
		this.hubConnection.on('ReceiveSpecificMessage', (message:any)=>{
			console.log(message);
			this.globalService.setUnreadMessagesNumber(this.globalService.unreadMessagesNumber()+1);
		});
		this.hubConnection.on('ValidationError', (message:any)=>{
			console.log(message);
			this.toast.error(message);
		});
	}

	public JoinChatWithUser = async (userId1: number, userId2: number) => {
		await this.hubConnection.invoke(
			'JoinChatWithUser', userId1, userId2
		);
	};


}
