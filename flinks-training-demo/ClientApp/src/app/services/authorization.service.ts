import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  instance: string = 'toolbox'
  customerId: string = '43387ca6-0391-4c82-857d-70d95f087ecb';
  public user: Customer;

  constructor(private http:HttpClient) { }
  /*
  onLogin(username: string, password: string):Observable<any>{
    const url = `http://${this.instance}-api.private.fin.ag/v3/${this.customerId}/BankingServices/Authorize/`;
    return this.http.post(url,   {
      "Institution":"FlinksCapital",
      "username": `${username}`,
      "Password": `${password}`,
      "MostRecentCached":false,
      "Save": true
    }, httpOptions);
  }*/
  

  onLogin(username: string, password: string):string{
    if (username === "Greatday" && password === "Everyday"){
      return "203";
    }
    return "400"
  }
}

interface Customer {
  username: string;
  requestId: string;
  securityChallenge: string;
  answer: string;
}