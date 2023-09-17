export type TransportRequestDto = {
    id: number,
    userId: 4,
    fullName: string,
    email: string,
    description: string,
    dateGap: DateGap,
    transportType: TransportType
    createdDate: Date
}

export type DateGap = {
    id: number,
    gap: string,
}

export type TransportType = {
    id: number,
    type: string,
}