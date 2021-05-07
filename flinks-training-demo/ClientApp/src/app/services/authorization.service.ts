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
  instance: string = 'toolbox';
  customerId: string = '43387ca6-0391-4c82-857d-70d95f087ecb';
  private baseUrl:string = 'blah-blah';
  private user:Customer;

  constructor(private http:HttpClient) { }
/*
  onLogin(username: string, password: string){
    var credentials = [username, password];
      
    this.http.post<Customer>(this.baseUrl + 'customer/login', credentials).subscribe(result => {
        this.user = result;
        if (this.user.username === null){
          console.log("Wrong credentials")
          return false;
        }
        console.log("this is users " + this.user.username);
        return true;
    }, error => console.error(error))
  }
*/
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
      return "400";
    }

    onSubmit(answer:string){
      if (answer === "Yes"){
        return "Good";
      }
      return "Bad";
    }
}

interface Customer {
  username: string;
  requestId: string;
  securityChallenge: string;
  answer: string;
}
