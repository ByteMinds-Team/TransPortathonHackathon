import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';

export type DateGap = {
  id: number
  gap: string
}

@Injectable({
  providedIn: 'root'
})
export class DateGapService {
  private readonly httpClient = inject(HttpClient)

  getDateGaps() {
    return this.httpClient.get<DateGap[]>(`${environment.apiUrl}DateGaps`)
  }
}
