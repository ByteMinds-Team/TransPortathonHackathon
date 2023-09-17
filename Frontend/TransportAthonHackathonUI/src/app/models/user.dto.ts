export interface UserDto {
    id: number
    fullName: string
    email: string
    imagePath: string
    operationClaims: OperationClaim[]
}

export type OperationClaim = {
    id: number
    name: string
}