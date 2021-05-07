import { Component, Inject } from '@angular/core';
import { HttpClient} from '@angular/common/http';


@Component({
  selector: 'app-fetch-detail',
  templateUrl: './fetch-detail.component.html',
  styleUrls: ['./fetch-detail.component.css']
})
export class FetchDetailComponent {

  public accountDetails: Array<AccountDetails>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    console.log("Account Detail started");
    http.get<Array<AccountDetails>>(baseUrl + 'customer/getaccountdetail').subscribe(result => {
        this.accountDetails = result;
        if (this.accountDetails[0].title == null)
          console.log("It failed :(")
        else
          console.log("this is title" + this.accountDetails[0].title);
           console.log("this is transaction" + this.accountDetails[0].transactions[0].date);
    }, error => console.error(error));

  }
}


interface AccountDetails{
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
  transactions: Transaction[];
}

interface Transaction {
  date: string;
  description: string;
  credit: number;
  debit: number;
  code: string;
  balance: number;
  id: string;
}