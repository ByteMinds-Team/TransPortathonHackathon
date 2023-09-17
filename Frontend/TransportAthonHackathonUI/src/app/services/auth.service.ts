import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { BehaviorSubject, filter, switchMap, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TokenResultDto } from '../models/token-result.dto';
import { UserDto } from '../models/user.dto';
import { JwtHelperService } from "@auth0/angular-jwt";
import { DecodedTokenDto } from "../models/decoded-token.dto";
import { RolesDto } from '../models/roles.dto';
import { Role } from '../models/role';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly httpClient = inject(HttpClient)
  private readonly jwtHelperService = inject(JwtHelperService)

  private readonly user$ = new BehaviorSubject<UserDto | null>(null)

  constructor() {
    if (this.checkIfLoggedIn()) {
      const user = localStorage.getItem('user')
      this.user$.next(JSON.parse(user!))
    }
  }

  login(credentials: { email: string, password: string }) {
    return this.httpClient.post<TokenResultDto>(`${environment.apiUrl}Auth/Login`, credentials).pipe(
      tap(tokenResult => {
        this.saveTokenResult(tokenResult)
      }),
      switchMap(tokenResult => this.getAdditionalUserData(tokenResult.token)),
      filter(user => user !== null),
      tap(user => {
        this.user$.next(user)
        localStorage.setItem('user', JSON.stringify(user))
      })
    )
  }

  register(formValue: any, image: File) {
    const formData = new FormData();
    formData.append('ProfilePhoto', image);
    formData.append('Email', formValue["email"]);
    formData.append('FullName', formValue["fullName"]);
    formData.append('Password', formValue["password"]);

    return this.httpClient.post<TokenResultDto>(`${environment.apiUrl}Auth/Register`, formData);
  }

  corporateRegister(formValue: any, image: File) {
    const formData = new FormData();
    formData.append('ProfilePhoto', image);
    formData.append('Email', formValue["email"]);
    formData.append('FullName', formValue["fullName"]);
    formData.append('CompanyName', formValue["companyName"]);
    formData.append('Password', formValue["password"]);

    return this.httpClient.post<TokenResultDto>(`${environment.apiUrl}Auth/CorporateRegister`, formData);
  }

  refreshToken() {
    const refreshToken = localStorage.getItem('refreshToken')
    const client = localStorage.getItem('client')
    const body = {
      refreshToken: refreshToken,
      client: client
    }
    return this.httpClient.post<TokenResultDto>(`${environment.apiUrl}auth/refreshToken`, body)
  }

  logout() {
    localStorage.clear();
  }

  get currentUser$() {
    return this.user$.asObservable()
  }

  private getAdditionalUserData(token: string) {
    return this.httpClient.get<UserDto>(`${environment.apiUrl}Auth/me`)
  }

  private checkIfLoggedIn() {
    const token = localStorage.getItem('accessToken');
    const user = localStorage.getItem('user')
    if (token && user)
      return true

    return false
  }

  private decodeToken() {
    const token = localStorage.getItem('accessToken')!;
    return this.jwtHelperService.decodeToken<DecodedTokenDto>(token);
  }

  getRoles() {
    const result = this.decodeToken();
    if (result)
      return result.roles;
    return null
  }

  saveTokenResult(tokenResult: TokenResultDto) {
    localStorage.setItem("accessToken", tokenResult.token)
    localStorage.setItem("tokenExpiration", tokenResult.tokenExpiration)
    localStorage.setItem("refreshToken", tokenResult.refreshTokenValue)
    localStorage.setItem("client", tokenResult.client)
  }

  isInRole(role: Role): boolean {
    if (role === null) {
      return true
    }
    const user = JSON.parse(localStorage.getItem('user')!) as UserDto;
    if (user && user.operationClaims.find(x => x.name == role))
      return true
    return false
  }

  getCorporateUserInformation(corporateUserId: number) {
    return this.httpClient.get<any>(`${environment.apiUrl}Auth/corporateUserInformation/${corporateUserId}`)
  }
}
