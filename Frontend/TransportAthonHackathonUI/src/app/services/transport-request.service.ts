import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TransportRequestDto } from '../models/transport-type.dto';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs';

const SPINNER = "s4"

@Injectable({
  providedIn: 'root'
})
export class TransportRequestService {
  private readonly httpClient = inject(HttpClient)
  private readonly spinnerService = inject(NgxSpinnerService)

  getTransportRequests() {
    this.spinnerService.show(SPINNER)
    return this.httpClient.get<TransportRequestDto[]>(`${environment.apiUrl}TransportRequests`)
      .pipe(
        finalize(() => this.spinnerService.hide(SPINNER))
      )
  }

  createTransportRequest(transportReq: any) {
    return this.httpClient.post<any>(`${environment.apiUrl}TransportRequests`, transportReq)
  }

}
