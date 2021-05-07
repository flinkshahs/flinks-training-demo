import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-fetch-answer',
  templateUrl: './fetch-answer.component.html',
  styleUrls: ['./fetch-answer.component.css']
})
export class FetchAnswerComponent {

  public messageMFA: MessageMFA;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
      
    console.log("Woohoo");
    console.log("MFA started");

    var answer = "Montreal";

    http.post<MessageMFA>(baseUrl + 'customer/answer', answer).subscribe(result => {
        this.messageMFA = result;
        if (this.messageMFA.message == null)
          console.log("It works!")
        else
          console.log("this is message " + this.messageMFA.message);
    }, error => console.error(error));

  }
}

interface MessageMFA {
  message: string;
}