import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeService } from 'src/app/services/employee.service';
import { EmployeeDto } from 'src/app/models/employee.dto';
import { CrewService } from 'src/app/services/crew.service';
import { CrewDto } from 'src/app/models/crew.dto';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { DriverService } from 'src/app/services/driver.service';
import { VehicleService } from 'src/app/services/vehicle.service';
import { VehicleDto } from 'src/app/models/vehicle.dto';
import { tap } from 'rxjs';
import { EmployeeWithVehicleDto } from 'src/app/models/employee-with-vehicle.dto';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
@Component({
    selector: 'app-corporate-crew',
    standalone: true,
    templateUrl: './corporate-crew.component.html',
    styleUrls: ['./corporate-crew.component.scss'],
    imports: [CommonModule,
        ReactiveFormsModule,
        FormsModule,
        HttpClientModule]
})
export class CorporateCrewComponent implements OnInit  {
  dropDownList : any; 
  dropDownListForCrew : any; 
  isOpenEmployeeModal = false;
  isOpenEmployeeUpdateModal = false;
  isOpenDriverModal = false;
  isOpenCrewModal = false;
  constructor(){}
  ngOnInit(): void {
    this.getAllEmployee();
    this.getAllCrewByCorporateUserId();
  }
  
  private readonly crewService = inject(CrewService)
  private readonly formBuilder = inject(FormBuilder)
  private readonly employeeService = inject(EmployeeService)
  private readonly toastrService = inject(ToastrService)
  private readonly driverService = inject(DriverService)
  private readonly vehicleService = inject(VehicleService)
  private readonly router = inject(Router)
  employeeForm = this.formBuilder.nonNullable.group({
    fullName: ["", [Validators.required]],
  })

  driverForm = this.formBuilder.nonNullable.group({
    vehicleId: [0, [Validators.required]],
  })

  crewForm = this.formBuilder.nonNullable.group({
    crewName: ["", [Validators.required]],
  })
  employeeIdForAddDriver : number;
  vehicleIdForAddDriver : number;
  employees: EmployeeWithVehicleDto[] = [];
  employee : EmployeeDto;
  optionEmployees: EmployeeDto[] = [];
  selectedValues: EmployeeDto[] = [];
  crews: CrewDto[] = [];
  vehicles : VehicleDto[];
  selectedFile: File | null = null;

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  addCrew(){
    const crewDto = {"name" : this.crewForm.value["crewName"] , "employeeIds" : this.selectedValues.map(i=>i.id)};
    this.crewService.addCrew(crewDto).subscribe(() => {
      this.crewService.getAllCrewByCorporateUserId().subscribe((data)=>{this.crews = data});
      this.changeCrewModalState(false);
  }, (err) =>{
    this.toastrService.error("Hata" , "Bir Sorun Oluştu");
  });
    }

    getVehicles(){
      this.vehicleService.getAllVehicleByCorporateId()
      .pipe(
        tap(vehicles => this.driverForm.patchValue({
          vehicleId: vehicles[0].id!
        })
        )
      ).subscribe((data) =>{
        this.vehicles = data;
      } , (err) =>{
        this.toastrService.error("Hata" , "Bir Sorun Oluştu");
      });
    }

  updateEmployee(){
    if(this.employeeForm.valid){
      this.employeeService.updateEmployee(this.employee.id , this.employeeForm.value["fullName"]!,this.selectedFile!).subscribe(()=>{
        this.changeEmployeeUpdateModalState(false , null);
        this.getAllEmployee();
      } , (err)=>{
        console.log(err);
        this.toastrService.error("Hata" , err["error"]["Errors"][0]["ErrorMessage"]);
      });
    }

    
    
  }
    
    deleteCrew(crewId:any){
      this.crewService.deleteCrew(crewId).subscribe(() =>{
        this.crewService.getAllCrewByCorporateUserId().subscribe((data)=>{this.crews = data})
    } , (err)=>{
      console.log(err);
      this.toastrService.error("Hata" ,"Bir Sorun Oluştu");
    });
      }

  addEmployee(){
    if(!this.employeeForm.valid){
      return;
    }

    if(this.selectedFile){
      this.employeeService.addEmployee(this.employeeForm.value["fullName"]! , this.selectedFile).subscribe(()=>{
        this.changeEmployeeModalState(false);
        this.getAllEmployee();
      }, (err)=>{
        this.toastrService.error("Hata" , "Bir Sorun Oluştu");
      });
    }
  }

  getAllEmployee(){
    this.employeeService.getAllEmployee().subscribe((response)=>{
      this.employees = response;
      this.optionEmployees = response;
    } , (err) =>{this.toastrService.error("Hata" , "Bir Sorun Oluştu");})
  }

  goVehicleDetailPage(vehicleId : number){
    this.router.navigate(['/home/vehicles', vehicleId])

  }

  getEmployeeById(id : number | null){
    this.employeeService.getEmployeeById(id).subscribe((response)=>{
      this.employee = response;
    },(err) =>{this.toastrService.error("Hata" , "Bir Sorun Oluştu");})
  }

  changeEmployeeModalState(value:boolean){
    this.isOpenEmployeeModal = value;
    
    
  }

  changeDriverModalState(value:boolean , employeeId : number | null){
    if(value == true && employeeId != null){
      this.employeeIdForAddDriver = employeeId;
      this.getVehicles();
    }
    this.dropDownList = -1;
    this.isOpenDriverModal = value;
    
    
  }

  deleteEmployee(employeeId : number){
    this.employeeService.deleteEmployee(employeeId).subscribe(()=>{
      
      this.getAllEmployee();
    })
  }

  changeEmployeeUpdateModalState(value:boolean, id : number | null){
    this.dropDownList = -1;
    if(value && id != null){
      this.getEmployeeById(id);
    }
    this.isOpenEmployeeUpdateModal = value;
  }

  changeCrewModalState(value:boolean){
    this.isOpenCrewModal = value;
  }
  

  
  getAllCrewByCorporateUserId(){
    this.crewService.getAllCrewByCorporateUserId().subscribe((response)=>{
      this.crews = response;
    } , (err) =>{console.log(err)})
  }

  onSelected(value:any){
    console.log(value);
    console.log(this.optionEmployees);
    this.optionEmployees = this.optionEmployees.filter(i => i.id !== +value)
    console.log(this.optionEmployees.filter(i => i.id !== +value));
    var data = this.employees.find(i=>i.id == +value);
    this.selectedValues.push(data!);
  }

  AddDriver(){
    this.driverService.addDriver(this.employeeIdForAddDriver ,+this.driverForm.value["vehicleId"]!).subscribe((data) => {
      this.changeDriverModalState(false , null);
    });
  }
  
  
}
