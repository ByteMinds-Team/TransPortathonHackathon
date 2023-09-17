import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@microsoft/signalr';
import { OfferService } from 'src/app/services/offer.service';
import { AuthService } from 'src/app/services/auth.service';
import { Role } from 'src/app/models/role';

@Component({
  selector: 'app-my-offer',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './my-offer.component.html',
  styleUrls: ['./my-offer.component.scss']
})
export class MyOfferComponent {
  private readonly offerService = inject(OfferService);
  private readonly authService = inject(AuthService);
  offers$ = this.offerService.getAllOfferByUserId();

  isInRole(role: Role): boolean {
    return this.authService.isInRole(role);
  }

  acceptOffer(offerId : number){
    this.offerService.acceptOffer(offerId).subscribe((data)=>{
       this.offers$ = this.offerService.getAllOfferByUserId();
    });
  }
}
