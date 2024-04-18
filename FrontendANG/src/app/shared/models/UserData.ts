export interface IUser {

}

export class User implements IUser{
	phoneNumber?: string;
	email?: string;
	name?: string;
	exp?: number;
}

export class Worker implements IUser {
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
