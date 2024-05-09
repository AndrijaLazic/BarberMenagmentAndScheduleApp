export interface IUser {
	isOnline:boolean;
}

export class User implements IUser{
	isOnline: boolean=false;
	phoneNumber?: string;
	email?: string;
	name?: string;
	exp?: number;
}

export class Worker implements IUser {
	isOnline: boolean=false;
	phoneNumber?: string;
	email?: string;
	name?: string;
	lastName?: string;
	workerTypeId?:WorkerType;
	id?:number;
	exp?: number;
}

enum WorkerType {
	Menadzer = 1,
	Frizer = 2,
}
