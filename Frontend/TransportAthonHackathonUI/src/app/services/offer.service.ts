import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Offer } from '../models/offer.dto';

@Injectable({
  providedIn: 'root'
})
export class OfferService {
  private readonly httpClient = inject(HttpClient)

  createOffer(offer: any) {
    return this.httpClient.post<any>(`${environment.apiUrl}Offers/CreateOffer`, offer)
  }

  getAllOfferByUserId() : Observable<Offer[]> {
    return this.httpClient.get<Offer[]>(`${environment.apiUrl}Offers/GetAllOfferByUserId`)
  }

  acceptOffer(offerId : number){
    return this.httpClient.post<any>(`${environment.apiUrl}Offers/AcceptOffer`, {'offerId' : offerId})
  }
}
