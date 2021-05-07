import { Component, Input } from '@angular/core';
import { AuthorizationService } from '../services/authorization.service';
import { FetchDataComponent } from '../fetch-data/fetch-data.component';
import { HttpClient } from '@angular/common/http';

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

  constructor(private authorizationService:AuthorizationService) {}

  // Set Dynamic Classes
  setClasses() {
    let classes = {
      'error': this.error,
      'authorized': this.authorized,
      firstBoxPlaceholder: this.firstBoxPlaceholder 
    }
    return classes;
  }
  /*
  onLogin(){
    console.log(this.username, this.password);
    this.authorizationService.onLogin(this.username, this.password).subscribe(request => {
      console.log(request);
    })
  }*/

 
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
  }

  onSubmit(){
    console.log("I can submit");
  }

}
