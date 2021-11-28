import { Device } from "./device";

export interface EventsDevice {
    date: string;
    message: string;
    isSeen: boolean;
    id: string;
    device: Device;
}