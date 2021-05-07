import { Component, Input, OnInit } from "@angular/core";
import { Transaction } from "../../models/Transaction";
import { Holder } from "../../models/Holder";
import { Account } from "../../models/Account";
import { AccountDetailsService } from "../../services/account-details.service";
import { FetchDetailComponent } from "../../fetch-detail/fetch-detail.component";
import { AccountDetails} from "../../models/AccountDetails";
// import { HeapProfiler } from "inspector";

@Component({
  selector: "app-balance-information",
  templateUrl: "./balance-information.component.html",
  styleUrls: ["./balance-information.component.css"],
})
export class BalanceInformationComponent implements OnInit {
  @Input() account?: AccountDetails;
  // account: Account;
  // transactions: Transaction[];
  // holder: Holder;
  // balance: number;

  constructor(private accountDetails: AccountDetailsService, private fetchDetailComponent: FetchDetailComponent) {}

  ngOnInit() {
    // this.balance = this.fetchDetailComponent.x;
    // this.account = this.accountDetails.getAccountInformation();
    // this.transactions = this.account.Transactions;
    // this.holder = this.account.Holder;
  }
}
