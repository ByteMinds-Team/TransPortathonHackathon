import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, finalize } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CrewDto } from '../models/crew.dto';
import { NgxSpinnerService } from 'ngx-spinner';
const SPINNER = "s4"
@Injectable({
  providedIn: 'root'
})
export class CrewService {
  private readonly httpClient = inject(HttpClient);
  private readonly spinnerService = inject(NgxSpinnerService);
  constructor() { }

  getAllCrewByCorporateUserId() : Observable<CrewDto[]>{
    this.spinnerService.show(SPINNER)
    return this.httpClient.get<CrewDto[]>(`${environment.apiUrl}Crews/GetAllCrewByCorporateUserId`).pipe(
      finalize(() => this.spinnerService.hide(SPINNER))
    )
    
  }

  addCrew(crew:any){
    return this.httpClient.post<any>(`${environment.apiUrl}Crews`,crew);
  }

  deleteCrew(crewId:number){
    console.log("crewId" , crewId)
    const formData = new FormData();
    formData.append('crewId',crewId.toString());
    return this.httpClient.post<any>(`${environment.apiUrl}Crews/DeleteCrew?crewId=${crewId}`,{});
  }
}
