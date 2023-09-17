import { VehicleDto } from "./vehicle.dto";

export type EmployeeWithVehicleDto = {
    id : number;
    fullName : string;
    profileImagePath : string;

    vehicle : VehicleDto[];
    
}