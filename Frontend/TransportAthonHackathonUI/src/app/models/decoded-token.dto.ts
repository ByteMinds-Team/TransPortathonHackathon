import {RolesDto} from "./roles.dto";

export type DecodedTokenDto = {
  sub: number
  email: string
  username: string
  roles: RolesDto[]
  iat: number
  exp: number
}
