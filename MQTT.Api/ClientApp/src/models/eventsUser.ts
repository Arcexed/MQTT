import { User } from "./user";

export interface EventsUser {
    date: string;
    message: string;
    isSeen: boolean;
    id: string;
    user: User;
}