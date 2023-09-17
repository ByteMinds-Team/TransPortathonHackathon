import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppointmentService } from 'src/app/services/appointment.service';
import { RouterModule } from '@angular/router';
import { Role } from 'src/app/models/role';
import { AuthService } from 'src/app/services/auth.service';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-appoinment',
  standalone: true,
  templateUrl: './appoinment.component.html',
  styleUrls: ['./appoinment.component.scss'],
  imports: [
    CommonModule,
    RouterModule, ReactiveFormsModule, FormsModule]
})
export class AppoinmentComponent {
  private readonly appointmentService = inject(AppointmentService)
  private readonly formBuilder = inject(FormBuilder);
  private readonly reviewService = inject(ReviewService);
  appointments$ = this.appointmentService.getAppointments()

  dropDownList: number;
  isOpen = false;
  offerId: number;
  reviewForm = this.formBuilder.nonNullable.group({
    comment: ["", [Validators.required]],
    point: [1, [Validators.required]],
  });

  createReview() {
    this.reviewService.createReview(this.reviewForm.value, this.offerId).subscribe();
    this.close();
  }

  close() {
    this.isOpen = false;
  }
  open(offerId: number) {
    this.offerId = offerId;
    this.isOpen = true;
  }

  calculateLeftDays(appointmentDate: Date) {
    return ((new Date(appointmentDate).getTime() - Date.now()) / (24 * 60 * 60 * 1000)).toFixed(0);
  }

  checkAppointmentStatusForReview(appointmentDate: Date) {
    var result = (new Date(appointmentDate).getTime() + 24 * 60 * 60 * 1000) <= Date.now();
    return result;
  }

  cancelOffer(offerId: number) {
    this.dropDownList = -1;
    return this.appointmentService.cancelAppointment(offerId).subscribe({
      next: _ => this.appointments$ = this.appointmentService.getAppointments()
    });
  }
}
