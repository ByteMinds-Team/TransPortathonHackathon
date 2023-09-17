export interface TokenResultDto {
    client: string,
    refreshTokenValue: string,
    token: string,
    tokenExpiration: string
}

export interface RefreshToken {
    token: string
    expires: Date
}