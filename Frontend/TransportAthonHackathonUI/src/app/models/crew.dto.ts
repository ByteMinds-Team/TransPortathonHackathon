import { EmployeeDto } from "./employee.dto";

export type CrewDto = {
    id : number;
    crewName : string;
    employees : EmployeeDto[];
}