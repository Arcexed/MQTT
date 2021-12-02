import {EventsDevice} from "./eventsDevice";
import {Measurement} from "./measurement";

export interface Device {
    name: string;
    creatingDate: string;
    editingDate: string | null;
    geo: string;
    description: string;
    publicIp: string | null;
    privateIp: string | null;
    mqttToken: string;
    isPublic: boolean;
    eventsDevices: EventsDevice[];
    measurements: Measurement[];
    visible: string;
    status: string;
    timespanStringLastMeasurement: string;
    id: string;
}