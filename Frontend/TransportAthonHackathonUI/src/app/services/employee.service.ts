import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, finalize } from 'rxjs';
import { EmployeeDto } from '../models/employee.dto';
import { environment } from 'src/environments/environment';
import { NgxSpinnerService } from 'ngx-spinner';
import { EmployeeWithVehicleDto } from '../models/employee-with-vehicle.dto';
const SPINNER = "s4"

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private readonly httpClient = inject(HttpClient);
  private readonly spinnerService = inject(NgxSpinnerService)

  getAllEmployee() : Observable<EmployeeWithVehicleDto[]>{
    return this.httpClient.get<EmployeeWithVehicleDto[]>(`${environment.apiUrl}Employees/GetAllEmployee`);
  }

  getEmployeeById(employeeId : number | null) : Observable<EmployeeDto>{
    return this.httpClient.get<EmployeeDto>(`${environment.apiUrl}Employees/GetByEmployeeId?employeeId=${employeeId}`);
  }

  addEmployee(fullName : string , employePhoto : File){
    const employee = new FormData();
    employee.append('FullName' , fullName);
    employee.append('ProfilePhoto' , employePhoto);
    return this.httpClient.post(`${environment.apiUrl}Employees/AddEmployee`,employee);
  }

  updateEmployee(employeeId : number , fullName : string, employePhoto : File){
    this.spinnerService.show(SPINNER)
    const employee = new FormData();
    employee.append('FullName' , fullName);
    employee.append('EmployeeId' , employeeId.toString());
    employee.append('ProfilePhoto' , employePhoto);
    return this.httpClient.post(`${environment.apiUrl}Employees/UpdateEmployee`,employee).pipe(
      finalize(() => this.spinnerService.hide(SPINNER))
    )
  }

  deleteEmployee(employeeId : number){
    this.spinnerService.show(SPINNER)
    const formData = new FormData();
    formData.append('employeeId' , employeeId.toString());
    return this.httpClient.post(`${environment.apiUrl}Employees/DeleteEmployee?employeeId=${employeeId}`,{}).pipe(
      finalize(() => this.spinnerService.hide(SPINNER))
    )
  }
}
