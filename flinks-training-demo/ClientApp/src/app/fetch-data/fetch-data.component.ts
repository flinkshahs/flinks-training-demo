import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public user: Customer;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    var credentials = ["Great4545345day", "Ever34545yday"]

    http.post<Customer>(baseUrl + 'customer', credentials).subscribe(result => {
        this.user = result;
        if (this.user.username == null)
          console.log("Wrong credentials")
        console.log("this is users " + result);
    }, error => console.error(error));
 

  }
}

interface Customer {
  username: string;
  requestId: string;
  securityChallenge: string;
  answer: string;
}
