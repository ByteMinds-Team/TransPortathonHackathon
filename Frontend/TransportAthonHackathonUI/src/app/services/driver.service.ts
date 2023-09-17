import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DriverService {
  private readonly httpClient = inject(HttpClient)
  addDriver(employeeId : number, vehicleId : number){
    const formData = new FormData();
    formData.append("VehicleId" , vehicleId.toString());
    formData.append("EmployeeId" , employeeId.toString());
    return this.httpClient.post(`${environment.apiUrl}Drivers/CreateDriver` , formData);
  }
  
  
}
