import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Customer } from '../interfaces/Customer';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

// @Injectable({
//   providedIn: 'root'
// })
export class AuthorizationService {
  public user: Customer;
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this.http = http;

    this.baseUrl = baseUrl;

  }

  onLogin(username: string, password: string) {

     var credentials = [username, password];

    this.http.post<Customer>(this.baseUrl + 'customer/login', credentials).subscribe(result => {
        this.user = result;
        if (this.user.username == null)
          console.log("Wrong credentials")
        console.log("this is users " + this.user.username);
    }, error => console.error(error))
   }




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
  
}