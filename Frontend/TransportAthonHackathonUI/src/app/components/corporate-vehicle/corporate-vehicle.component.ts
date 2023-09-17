import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VehicleService } from 'src/app/services/vehicle.service';
import { VehicleDto } from 'src/app/models/vehicle.dto';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-corporate-vehicle',
  standalone: true,
  imports: [CommonModule,
    ReactiveFormsModule,
    RouterModule,
    FormsModule,
    HttpClientModule],
  templateUrl: './corporate-vehicle.component.html',
  styleUrls: ['./corporate-vehicle.component.scss']
})
export class CorporateVehicleComponent implements OnInit{
  private readonly vehicleService = inject(VehicleService)
  private readonly formBuilder = inject(FormBuilder)
  vehicles : VehicleDto[] = [];
  vehicleForm = this.formBuilder.nonNullable.group({
    numberPlate: ["", [Validators.required]],
    brand: ["", [Validators.required]],
    color: ["", [Validators.required]],
    modelYear: ["", [Validators.required]],
  })
  selectedFile: File | null = null;
  isOpen = false;


  ngOnInit(): void {
   this.getAllVehicleByCorporateId();
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }
  
  changeModalState(value:boolean){
    this.isOpen = value;
  }


  getAllVehicleByCorporateId() {
    this.vehicleService.getAllVehicleByCorporateId().subscribe((response)=>{
      this.vehicles = response;
    });
  }

  addVehicle(){
    if(!this.vehicleForm.valid){
      return;
    }

    if(this.selectedFile){
      this.vehicleService.addVehicle(this.vehicleForm.value , this.selectedFile).subscribe(()=>{
        this.changeModalState(false);
      });
    }
  }
}
