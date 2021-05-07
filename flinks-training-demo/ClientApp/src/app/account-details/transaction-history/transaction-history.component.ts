import { Component, OnInit } from '@angular/core';
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
  account: Account;
  transactions: Transaction[];
  holder: Holder;

  constructor(private accountDetails: AccountDetailsService) { }

  ngOnInit() {
    this.account = this.accountDetails.getAccountInformation();
    this.transactions = this.account.Transactions;
    this.holder = this.account.Holder;
  }

  getIncome() {
    let sum = 0;
    this.transactions.forEach((t) => {
      if (t.Credit != null) {
        sum += t.Credit;
      }
    });
    return sum;
  }

  getExpense() {
    let sum = 0;
    this.transactions.forEach((t) => {
      if (t.Debit != null) {
        sum += t.Debit;
      }
    });
    return sum;
  }

  getTransactionArrow(transaction: Transaction) {
    if (transaction.Debit == null) {
      return "fa-long-arrow-down";
    } else {
      return "fa-long-arrow-up";
    }
  }
  getAmount(transaction: Transaction) {
    if (transaction.Debit == null) {
      return transaction.Credit;
    } else {
      return -transaction.Debit;
    }
  }

}
