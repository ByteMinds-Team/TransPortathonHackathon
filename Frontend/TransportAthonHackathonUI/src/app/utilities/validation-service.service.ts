import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ValidationService {

  constructor() { }

  validate(error:any,key:any){
    let keys = Object.keys(error);
    if (keys[0] === "required") {
      return key + " field is required";
    }

    if (keys[0] === "email") {
      return key + " field must be a valid email";
    }
    return "there is a different error";
  }
}
