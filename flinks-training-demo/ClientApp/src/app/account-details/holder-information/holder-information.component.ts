import { Component, Input, OnInit } from '@angular/core';
import { Transaction } from "../../models/Transaction";
import { Holder } from "../../models/Holder";
import { Account } from "../../models/Account";
import { AccountDetails} from "../../models/AccountDetails"
import { AccountDetailsService } from "../../services/account-details.service";

@Component({
  selector: 'app-holder-information',
  templateUrl: './holder-information.component.html',
  styleUrls: ['./holder-information.component.css']
})
export class HolderInformationComponent implements OnInit {
  @Input() account?: AccountDetails;
  // account: Account;
  transactions: Transaction[];
  holder: Holder;

  constructor(private accountDetails: AccountDetailsService) {}

  ngOnInit() {
    // this.account = this.accountDetails.getAccountInformation();
    // this.transactions = this.account.Transactions;
    // this.holder = this.account.Holder;
  }
}
