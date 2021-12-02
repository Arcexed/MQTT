import {Role} from "./role";
import {Device} from "./device";
import {EventsUser} from "./eventsUser";

export interface User {
    username: string;
    password: string;
    email: string;
    ip: string;
    isBlock: boolean;
    accessToken: string;
    role: Role;
    devices: Device[];
    eventsUsers: EventsUser[];
    id: string;
}