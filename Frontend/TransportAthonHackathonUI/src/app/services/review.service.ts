import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Review } from '../models/review';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  private readonly httpClient = inject(HttpClient)

  getReviewsByCorporateCustomerId(corporateCustomerId: number) {
    return this.httpClient.get<Review[]>(`${environment.apiUrl}Reviews/GetAllReviewByCorporateUserId?CorporateCustomerId=${corporateCustomerId}`)
  }
  
  createReview(review: any, offerId: number) {
    review.offerId = offerId;
    return this.httpClient.post<any>(`${environment.apiUrl}Reviews/CreateReview`, review);
  }
}
