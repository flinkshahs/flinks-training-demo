import { Component, Input, Inject } from '@angular/core';
// import { AuthorizationService } from '../services/authorization.service';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../interfaces/Customer';
import { MessageMFA } from '../interfaces/MessageMFA';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  @Input() username: string;
  @Input() password: string;
  @Input() answer: string = "blah";
  error: boolean = false;
  error2: boolean = false;
  errorMessage: string = "Error in Login"
  authorized: boolean = false;
  firstBoxPlaceholder: string = "Username";
  buttonText: string = "Log In";
  securityQuestion: string;
  public user: Customer;
  private http: HttpClient;
  private baseUrl: string;
  public messageMFA: MessageMFA;
  private router: Router;

  constructor(router: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this.http = http;
    this.router = router;
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
    console.log(this.username, this.password);

    var credentials = [this.username, this.password];

    this.http.post<Customer>(this.baseUrl + 'customer/login', credentials).subscribe(result => {
        this.user = result;
        if (this.user.username == null)
            this.error = true;
        else {
          this.authorized = true;
          this.securityQuestion = this.user.securityChallenge;
          this.firstBoxPlaceholder = "Answer";
          this.buttonText = "Enter"
          this.username = "";
          this.password = "";
          this.error = false;
        }
        console.log("this is users " + this.user.username);
    }, error => console.error(error))
 
  }

  onSubmit(){
    console.log("I can submit");
    console.log(this.answer);
    if (this.answer == null || this.answer == "")
      this.answer = "blah";
    console.log("Woohoo");
    console.log("MFA started");
    var answer = [this.answer];
    console.log("This is answer" + this.answer);
    this.http.post<MessageMFA>(this.baseUrl + 'customer/answer', answer).subscribe(result => {
        this.messageMFA = result;
        if (this.messageMFA.message == null)
        {
          console.log("It works!");
          this.error2 = false;
          this.router.navigateByUrl('/');
        }
        else
        {
          this.errorMessage = this.messageMFA.message;
          this.error2 = true;
          console.log("this is message " + this.messageMFA.message);
        }
    }, error => console.error(error));
  }

}
