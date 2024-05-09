import { Worker } from "../UserData";
import { SingleMessage } from "./singleMessage";

export class CurrentChat{
	user:Worker | null=null;
	messages:SingleMessage[]=[];
}
