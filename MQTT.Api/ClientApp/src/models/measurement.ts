import {Device} from "./device";

export interface Measurement {
    id: string;
    date: string;
    atmosphericPressure: number;
    temperature: number;
    airHumidity: number;
    lightLevel: number;
    smokeLevel: number;
    radiationLevel: number;
    device: Device;
}