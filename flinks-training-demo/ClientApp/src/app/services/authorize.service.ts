import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MFA_answer } from '../models/MFA_answer';

@Injectable({
  providedIn: 'root'
})

export class AuthorizeService {
  authURL = 'https://toolbox-api.private.fin.ag/v3/43387ca6-0391-4c82-857d-70d95f087ecb/BankingServices/Authorize'

  postData = {
    "RequestId": "a10fbd41-d942-4a1f-b202-16f63b5a9a81",
    "SecurityResponses": {
        "What shape do people like most?": [
            "Triangle"
        ],
        "What is the best country on earth?": [
            "Canada"
        ],
        "What city were you born in?" : [
            "Montreal"
        ]
    }
  };

  json;

  constructor(private http:HttpClient) { }
  
  //Submit MFA answer
  addMFAanswer() {
    this.http.post(this.authURL, this.postData).toPromise().then((data:any) => {
      console.log(data);
      this.json = JSON.stringify(data.json);
    });
  }

  //this will be replace by backend POST request response
  getAuthorizeResponse() {
    return {
      "Links": [
          {
              "rel": "Authorization",
              "href": "/Authorize",
              "example": null
          }
      ],
      "HttpStatusCode": 203,
      "SecurityChallenges": [
          {
              "Type": "QuestionAndAnswer",
              "Prompt": "What is the best country on earth?"
          }
      ],
      "Institution": "FlinksCapital",
      "RequestId": "a10fbd41-d942-4a1f-b202-16f63b5a9a81"
    }
  }


}