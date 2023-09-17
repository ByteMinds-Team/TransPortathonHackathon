import { TransportRequestDto, TransportType } from "./transport-type.dto"
import { VehicleDto } from "./vehicle.dto"

export type Offer = {
    id: number
    price: number
    companyName: string
    companyImagePath: string
    corporateUserFullName: string
    corporateCustomerProfilePhotoPath: string
    appointmentDate: Date
    description: string
    corporateCustomerId: number
    transportRequestId: number
    crewId: number
    isAccepted: boolean
    isCanceled: boolean
    crew: Crew
    employees: any[]
    vehicles: VehicleDto[]
    transportRequest: TransportRequestDto
}

export type Crew = {
    name: string
    id: number
    createdDate: Date
    updatedDate: Date
}