import { Component, Input, Inject } from '@angular/core';
// import { AuthorizationService } from '../services/authorization.service';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../interfaces/Customer';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  @Input() username: string;
  @Input() password: string;
  @Input() answer: string;
  error: boolean = false;
  authorized: boolean = false;
  firstBoxPlaceholder: string = "Username";
  buttonText: string = "Log In";
  public user: Customer;
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this.http = http;

    this.baseUrl = baseUrl;

  }

  setClasses() {
    let classes = {
      'error': this.error,
      'authorized': this.authorized,
      firstBoxPlaceholder: this.firstBoxPlaceholder 
    }
    return classes;
  }

 
  onLogin(){
    if (this.authorized){
      this.onSubmit();
      return;
    } 
    if (this.authorizationService.onLogin(this.username, this.password) !== "203"){
      this.error = true;
    } else{
      this.authorized = true;
      this.firstBoxPlaceholder = "Answer";
      this.buttonText = "Enter"
      this.username = "";
      this.password = "";
    }
    console.log(this.username, this.password);

    var credentials = [this.username, this.password];

    this.http.post<Customer>(this.baseUrl + 'customer/login', credentials).subscribe(result => {
        this.user = result;
        if (this.user.username == null)
            this.error = true;
        console.log("this is users " + this.user.username);
        // move to next screen
    }, error => console.error(error))
 
  }

  onSubmit(){
    console.log("I can submit");
  }

}
