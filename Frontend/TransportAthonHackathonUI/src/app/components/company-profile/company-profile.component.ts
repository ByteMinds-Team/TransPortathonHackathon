import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/app/services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-company-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './company-profile.component.html',
  styleUrls: ['./company-profile.component.scss']
})
export class CompanyProfileComponent {
  private readonly authService = inject(AuthService)
  private readonly activatedRoute = inject(ActivatedRoute)
  private readonly reviewService = inject(ReviewService)

  companyInfo$ = this.activatedRoute.params.pipe(
    switchMap(params => this.authService.getCorporateUserInformation(+params['id']))
  )

  reviews$ = this.activatedRoute.params.pipe(
    switchMap(params => this.reviewService.getReviewsByCorporateCustomerId(+params['id']))
  )
}