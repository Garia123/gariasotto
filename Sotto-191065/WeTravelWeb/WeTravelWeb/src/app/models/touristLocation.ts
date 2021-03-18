import { Lodging } from 'src/app/models/lodging';
import { Region } from './region';
import { TouristLocationCategory } from './touristLocationCategory';
import { Image } from './image';

export class TouristLocation {
    name:string;
    description:string;
    image: Image;
    region: Region;
    lodgings: Array<Lodging>;
    touristLocationCategory: Array<TouristLocationCategory>;
}

