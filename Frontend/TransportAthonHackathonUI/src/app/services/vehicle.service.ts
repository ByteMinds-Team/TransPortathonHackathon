import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, finalize } from 'rxjs';
import { environment } from 'src/environments/environment';
import { VehicleDto } from '../models/vehicle.dto';
import { NgxSpinnerService } from 'ngx-spinner';
const SPINNER = "s4"
@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private readonly httpClient = inject(HttpClient)
  private readonly spinnerService = inject(NgxSpinnerService)
  constructor() { }

  addVehicle(vehicle : any , file : File){
    const formData = new FormData();
    formData.append('NumberPlate',vehicle["numberPlate"])
    formData.append('Brand',vehicle["brand"])
    formData.append('Color',vehicle["color"])
    formData.append('ModelYear',vehicle["modelYear"])
    formData.append('CarImage',file)
    return this.httpClient.post(`${environment.apiUrl}Vehicles`,formData)
  }

  getAllVehicleByCorporateId() : Observable<VehicleDto[]>{
    this.spinnerService.show(SPINNER)
    return this.httpClient.get<VehicleDto[]>(`${environment.apiUrl}Vehicles`)
    .pipe(
      finalize(() => this.spinnerService.hide(SPINNER))
    )
  }

  getVehicleByVehicleId(vehicleId : number) : Observable<VehicleDto>{
    this.spinnerService.show(SPINNER)
    return this.httpClient.get<VehicleDto>(`${environment.apiUrl}Vehicles/GetVehicleById?vehicleId=${vehicleId}`)
    .pipe(
      finalize(() => this.spinnerService.hide(SPINNER))
    )
  }
}
