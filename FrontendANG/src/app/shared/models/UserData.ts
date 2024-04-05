export interface IUser {

}

export class User implements IUser{
	PhoneNumber?: string;
	Email?: string;
	Name?: string;
	exp?: number;
}

export class Worker implements IUser {
	PhoneNumber?: string;
	Email?: string;
	Name?: string;
	LastName?: string;
	WorkerTypeId?:WorkerType;
	Id?:number;
	exp?: number;
}

enum WorkerType {
	Menadzer = 1,
	Frizer = 2,
}
