import { Component, Input, Inject } from '@angular/core';
//import { AuthorizeService } from '../services/authorize.service';
//import { MFA_answer } from '../models/MFA_answer';
//import { Authorize_Response } from '../models/Authorize_Response';
//import { SecurityChallenges } from '../models/SecurityChallenges';
//import { SecurityResponses } from '../models/SecurityResponses';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../interfaces/Customer';
import { MessageMFA } from '../interfaces/MessageMFA';

@Component({
  selector: 'app-security-question',
  templateUrl: './security-question.component.html',
  styleUrls: ['./security-question.component.css']
})
export class SecurityQuestionComponent {
  
  @Input() answer: string;
  error: boolean = false;
  public user: Customer;
  private http: HttpClient;
  private baseUrl: string;
  public messageMFA: MessageMFA;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    this.http = http;

    this.baseUrl = baseUrl;

  }

  setClasses() {
    let classes = {
      'error': this.error,
    }
    return classes;
  }

  onSubmit() {

    console.log(this.answer);

    console.log("Woohoo");
    console.log("MFA started");
    var answer = [this.answer];
    this.http.post<MessageMFA>(this.baseUrl + 'customer/answer', answer).subscribe(result => {
        this.messageMFA = result;
        if (this.messageMFA.message == null)
          console.log("It works!")
        else
          console.log("this is message " + this.messageMFA.message);
    }, error => console.error(error));
 
  }

  /*
  answer: string;
  authResponse: Authorize_Response;
  securityAnswer: MFA_answer;
  prompt: string;
  requestID: string;

  constructor(private authorizeService:AuthorizeService) { }

  ngOnInit() {
    // pass in Security Challenge prompt from Login Component Authorize Call 
    this.authResponse = this.authorizeService.getAuthorizeResponse();
    this.prompt = this.authResponse.SecurityChallenges[0].Prompt;
    // store Request Id
    this.requestID = this.authResponse.RequestId;
    console.log(this.authResponse);
    console.log(this.prompt);
    console.log(this.requestID);
  }

  onSubmit() {
    //this.authorizeService.addMFAanswer();
    console.log('submit');
    // pass form answer to Authorize with Request Id
    this.securityAnswer = {
      RequestId: this.authResponse.RequestId,
      SecurityResponses: {
        
      }
    }
    console.log(this.securityAnswer);
    // if successful, redirect to a page with GetAccountSummary and GetAccountDetail
  }
  */
}
