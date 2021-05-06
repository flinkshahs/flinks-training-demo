import { Component, Inject } from '@angular/core';
import { HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-fetch-summary',
  templateUrl: './fetch-summary.component.html',
  styleUrls: ['./fetch-summary.component.css']
})
export class FetchSummaryComponent{

  public accountSummary: Array<AccountSummary>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    console.log("Account Summary started");
    http.get<Array<AccountSummary>>(baseUrl + 'customer/getaccountsummary').subscribe(result => {
        this.accountSummary = result;
        if (this.accountSummary[0].title == null)
          console.log("It failed :(")
        else
          console.log("this is title" + this.accountSummary[0].title);
    }, error => console.error(error));

  }
}


interface AccountSummary{
  title: string;
  accountNumber: string;
  availableBalance: number;
  currentBalance: number;
  limitBalance: number;
  category: string;
  currency: string;
  name: string;
  civicAddress: string;
  city: string;
  province: string;
  postalCode: string;
  Country: string;
  phoneNumber: string,
  email: string,
  id: string;
}