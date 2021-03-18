import { Lodging } from './lodging';
import { ReserveDescription } from '../models/reserveDescription';

export class Reserve {
    checkIn: Date;
    checkOut: Date;
    adults: number;
    children: number;
    babies: number;
    price: number;
    telefono: String;
    informationText: string;
    contactFirstName: string;
    contactLastName: string;
    contactEmail: string;
    lodgings: Lodging;
    reserveDescription: ReserveDescription;
}
