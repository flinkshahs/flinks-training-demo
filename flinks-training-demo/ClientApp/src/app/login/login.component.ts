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
  error: boolean = false;

  constructor(private authorizationService:AuthorizationService) {}

  // Set Dynamic Classes
  setClasses() {
    let classes = {
      'error': this.error,
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
    console.log(this.username, this.password);
    if (this.authorizationService.onLogin(this.username, this.password) !== "203"){
      this.error = true;
    }
    console.log(this.authorizationService.onLogin(this.username, this.password));
  }



}
