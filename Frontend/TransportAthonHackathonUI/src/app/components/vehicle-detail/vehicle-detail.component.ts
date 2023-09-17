import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VehicleService } from 'src/app/services/vehicle.service';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs';

@Component({
  selector: 'app-vehicle-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './vehicle-detail.component.html',
  styleUrls: ['./vehicle-detail.component.scss']
})
export class VehicleDetailComponent {
  private readonly vehicleService = inject(VehicleService)
  private readonly activatedRoute = inject(ActivatedRoute)
  vehicle$ = this.activatedRoute.params.pipe(
    switchMap(params => this.vehicleService.getVehicleByVehicleId(+params['id']))
  )

}
