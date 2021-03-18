import { TouristLocation } from 'src/app/models/touristLocation';
import { Image } from 'src/app/models/image';
import { Reserve } from 'src/app/models/reserve'

export class Lodging {
    id:string;
    name: string;
    stars: number;
    address: string;
    description: string;
    pricePerNight: number;
    available: boolean;
    telephone: string;
    informationText: string;
    images:Array<Image>;
    touristLocation:TouristLocation;
    reserves:Array<Reserve>;
}
