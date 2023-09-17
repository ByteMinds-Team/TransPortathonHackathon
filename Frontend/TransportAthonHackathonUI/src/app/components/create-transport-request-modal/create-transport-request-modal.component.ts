import { Component, EventEmitter, Input, Output, inject } from '@angular/core';
import { AsyncPipe, CommonModule, NgFor, NgIf } from '@angular/common';
import { TransportTypeService } from 'src/app/services/transport-type.service';
import { DateGapService } from 'src/app/services/date-gap.service';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { TransportRequestService } from 'src/app/services/transport-request.service';
import { tap } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

const { required } = Validators

@Component({
  selector: 'app-create-transport-request-modal',
  standalone: true,
  imports: [
    NgFor,
    AsyncPipe,
    NgIf,
    ReactiveFormsModule
  ],
  templateUrl: './create-transport-request-modal.component.html',
  styleUrls: ['./create-transport-request-modal.component.scss']
})
export class CreateTransportRequestModalComponent {
  private readonly formBuilder = inject(NonNullableFormBuilder)
  private readonly transportRequestService = inject(TransportRequestService)
  private readonly toastrService = inject(ToastrService)

  @Input() isOpen: boolean;
  @Output() onClose: EventEmitter<any> = new EventEmitter<any>()
  @Output() onSuccess: EventEmitter<any> = new EventEmitter<any>()

  transportRequestForm = this.formBuilder.group({
    title: ["", required],
    description: ["", required],
    dateGapId: [0, required],
    transportTypeId: [0, required]
  })

  transportTypes$ = inject(TransportTypeService).getTransportTypes().pipe(
    tap(transportType => this.transportRequestForm.patchValue({ transportTypeId: transportType[0].id }))
  )

  dateGaps$ = inject(DateGapService).getDateGaps().pipe(
    tap(dateGaps => this.transportRequestForm.patchValue({ dateGapId: dateGaps[0].id }))
  )

  close() {
    this.onClose.emit()
  }

  createTransportRequest() {
    if (this.transportRequestForm.valid) {
      this.transportRequestService.createTransportRequest(this.transportRequestForm.value)
        .subscribe({
          next: _ => this.onSuccess.emit()
        });
    }else this.toastrService.warning("Taşıma isteğini yaratmadan önce alanları doldurmalısın.", "Uyarı")
  }
}