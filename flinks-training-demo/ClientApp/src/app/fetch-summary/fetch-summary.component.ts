import { Component, Inject } from '@angular/core';
import { HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-fetch-summary',
  templateUrl: './fetch-summary.component.html',
  styleUrls: ['./fetch-summary.component.css']
})
export class FetchSummaryComponent{

  public accountSummary: AccountSummary;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    console.log("Account Summary started");
    http.get<AccountSummary>(baseUrl + 'customer/getaccountsummary').subscribe(result => {
        this.accountSummary = result;
        if (this.accountSummary.transitNumber == null)
          console.log("It failed :(")
        else
          console.log("this is transit number " + this.accountSummary.transitNumber);
    }, error => console.error(error));

  }
}


interface AccountSummary{
  transitNumber: number;
  institutionNumber: number;
  overdraftLimit: number;
  title: string;
  accountNumber: string;
  availableBalance: number;
  currentBalance: number;
  limitBalance: number;
  category: string;
  type: string;
  currency: string;
  holder: string;
  loginId: string;
}