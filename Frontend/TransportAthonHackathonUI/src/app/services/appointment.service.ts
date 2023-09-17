import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Offer } from '../models/offer.dto';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {
  private readonly httpClient = inject(HttpClient)

  getAppointments() {
    return this.httpClient.get<Offer[]>(`${environment.apiUrl}Offers/GetAllAcceptedOfferByUserId`)
  }

  getAppointment(offerId: number) {
    return this.httpClient.get<Offer>(`${environment.apiUrl}Offers/${offerId}`)
  }

  cancelAppointment(offerId: number) {
    return this.httpClient.post<any>(`${environment.apiUrl}Offers/CancelOffer`, {
      offerId: offerId
    })
  }

}
