import { Component, EventEmitter, Input, OnInit, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { tap } from 'rxjs';
import { CrewService } from 'src/app/services/crew.service';
import { VehicleService } from 'src/app/services/vehicle.service';
import { ActivatedRoute } from '@angular/router';
import { OfferService } from 'src/app/services/offer.service';

const { required, min, max } = Validators

@Component({
  selector: 'app-create-offer-modal',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './create-offer-modal.component.html',
  styleUrls: ['./create-offer-modal.component.scss']
})
export class CreateOfferModalComponent {
  private readonly formBuilder = inject(NonNullableFormBuilder)
  private readonly crewService = inject(CrewService)
  private readonly vehicleService = inject(VehicleService)
  private readonly toastrService = inject(ToastrService)
  private readonly activatedRoute = inject(ActivatedRoute)
  private readonly offerService = inject(OfferService)

  @Input() isOpen: boolean = false;
  @Output() onClose: EventEmitter<any> = new EventEmitter<any>()
  @Output() onSuccess: EventEmitter<any> = new EventEmitter<any>()

  offerForm = this.formBuilder.group({
    price: [null, [required, min(0), max(990000)]],
    appointmentDate: [null, [required]],
    description: ["", required],
    transportRequestId: [0, required],
    vehicleId: [0, required],
    crewId: [0, required]
  })

  vehicles$ = this.vehicleService.getAllVehicleByCorporateId().pipe(
    tap(vehicles => this.offerForm.patchValue({ vehicleId: vehicles[0].id}))
  )

  crews$ = this.crewService.getAllCrewByCorporateUserId().pipe(
    tap(crews => this.offerForm.patchValue({ crewId: crews[0].id }))
  )

  constructor() {
    this.offerForm.valueChanges.subscribe(value => console.log(value))
    this.activatedRoute.params.subscribe({
      next: params => this.offerForm.patchValue({
        transportRequestId: +params['id']
      })
    })
  }

  close() {
    this.onClose.emit()
  }

  createOffer() {
    this.offerService.createOffer(this.offerForm.value)
      .subscribe({
        next: _ => this.close(),
        error: _ => this.toastrService.error("Bir sorun oluştu.", "Hata")
      })
  }
}
