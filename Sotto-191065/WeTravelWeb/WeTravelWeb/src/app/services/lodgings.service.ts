import { Injectable } from '@angular/core';
import { Lodging } from 'src/app/models/lodging';
import { ApiService } from './api.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class LodgingsService {
  lodgings: Lodging[];

  constructor(private apiService: ApiService) { }

  getLodgings() {
    return this.apiService.get<Lodging[]>('api/lodgings')
      .pipe(map(data => {
        this.lodgings = data;
      }));
  }

  addLodgings(lodging: Lodging) {
    return this.apiService.put('api/lodgings/' + lodging.id, {
      "Name": lodging.name,
      "Stars": lodging.stars,
      "Address": lodging.address,
      "Description": lodging.description,
      "PricePerNight": lodging.pricePerNight,
      "Available": lodging.available,
      "Telephone": lodging.telephone,
      "InformationText": lodging.informationText,
      "TouristLocation": lodging.touristLocation
    });
  }

  deleteLodging(lodging: Lodging) {
    return this.apiService.delete('api/lodgings/'+ lodging.id);
  }
  
}
