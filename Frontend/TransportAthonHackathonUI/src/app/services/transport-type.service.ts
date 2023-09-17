import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';

export type TransportType = {
  id: number,
  type: string
}

@Injectable({
  providedIn: 'root'
})
export class TransportTypeService {
  private readonly httpClient = inject(HttpClient)

  getTransportTypes() {
    return this.httpClient.get<TransportType[]>(`${environment.apiUrl}TransportTypes`);
  }
}
