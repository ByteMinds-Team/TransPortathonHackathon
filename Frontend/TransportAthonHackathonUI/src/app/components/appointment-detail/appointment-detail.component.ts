import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppointmentService } from 'src/app/services/appointment.service';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs';

@Component({
  selector: 'app-appointment-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './appointment-detail.component.html',
  styleUrls: ['./appointment-detail.component.scss']
})
export class AppointmentDetailComponent {
  private readonly appointmentService = inject(AppointmentService);
  private readonly activatedRoute = inject(ActivatedRoute)
  appointment$ = this.activatedRoute.params.pipe(
    switchMap(params => this.appointmentService.getAppointment(+params['id']))
  )
}