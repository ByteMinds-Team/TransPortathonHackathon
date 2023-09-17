import { AsyncPipe, CommonModule, DatePipe, NgFor, NgIf } from '@angular/common';
import { Component, inject, signal, effect } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { HomeLayoutComponent } from 'src/app/layouts/home-layout/home-layout.component';
import { TransportRequestService } from 'src/app/services/transport-request.service';
import { CreateTransportRequestModalComponent } from "../create-transport-request-modal/create-transport-request-modal.component";
import { CreateOfferModalComponent } from "../create-offer-modal/create-offer-modal.component";
import { TransportRequestDto } from 'src/app/models/transport-type.dto';
import { AuthService } from 'src/app/services/auth.service';
import { Role } from 'src/app/models/role';
import { MessageService } from 'src/app/services/message.service';

const SPINNER = "s1"

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  standalone: true,
  imports: [
    NgFor,
    NgIf,
    AsyncPipe,
    DatePipe,
    RouterModule,
    HomeLayoutComponent,
    CreateTransportRequestModalComponent,
    CreateOfferModalComponent
  ]
})
export class HomeComponent {
  private readonly transportRequestService = inject(TransportRequestService);
  private readonly authService = inject(AuthService)
  private readonly messageService = inject(MessageService)
  private readonly router = inject(Router)

  transportRequests$ = this.transportRequestService.getTransportRequests();
  isModalOpen = signal<boolean>(false);

  isCreateOfferModalOpen = signal<boolean>(false);

  openModal() {
    this.isModalOpen.set(true)
  }

  closeModal() {
    this.isModalOpen.set(false)
    this.isCreateOfferModalOpen.set(false)
  }

  closeModalAndFetchLatestTransportRequests() {
    this.transportRequests$ = this.transportRequestService.getTransportRequests();
    this.closeModal();
  }

  openCreateOfferModal(transportRequest: TransportRequestDto) {
    this.router.navigate(['/home/offers', transportRequest.id])
    this.isCreateOfferModalOpen.set(true);
  }

  isInRole(role: Role): boolean {
    return this.authService.isInRole(role);
  }

  startChat(userId: number) {
    this.messageService.sendMessage({
      text: "Merhaba",
      receiverId: userId
    }).subscribe({
      next: _ => this.router.navigate(['/home/messages'])
    })
  }
}
