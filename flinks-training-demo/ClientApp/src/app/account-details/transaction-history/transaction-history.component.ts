import { Component, Input, OnInit } from '@angular/core';
import { Transaction } from "../../models/Transaction";
import { Holder } from "../../models/Holder";
import { Account } from "../../models/Account";
import { AccountDetailsService } from "../../services/account-details.service";
import { AccountDetails} from "../../models/AccountDetails";

@Component({
  selector: 'app-transaction-history',
  templateUrl: './transaction-history.component.html',
  styleUrls: ['./transaction-history.component.css']
})
export class TransactionHistoryComponent implements OnInit {
  @Input() account?: AccountDetails;
  // account: Account;
  transactions: Transaction[];
  // holder: Holder;

  constructor(private accountDetails: AccountDetailsService) { }

  ngOnInit() {
    // this.account = this.accountDetails.getAccountInformation();
    this.transactions = this.account.transactions;
    // this.holder = this.account.Holder;
  }

  getIncome() {
    let sum = 0;
    this.transactions.forEach((t) => {
      if (t.credit != null) {
        sum += t.credit;
      }
    });
    return sum;
  }

  getExpense() {
    let sum = 0;
    this.transactions.forEach((t) => {
      if (t.debit != null) {
        sum += t.debit;
      }
    });
    return sum;
  }

  getTransactionArrow(transaction: Transaction) {
    if (transaction.debit == null) {
      return "fa-long-arrow-down";
    } else {
      return "fa-long-arrow-up";
    }
  }
  getAmount(transaction: Transaction) {
    if (transaction.debit == null) {
      return transaction.credit;
    } else {
      return -transaction.debit;
    }
  }

}
