import { Component, OnInit } from '@angular/core';
import { InvestmentTransaction} from '../../models/InvestmentTransaction'
import { InvestmentsService } from '../../services/investments.service'

@Component({
  selector: 'app-inv-transaction-history',
  templateUrl: './inv-transaction-history.component.html',
  styleUrls: ['./inv-transaction-history.component.css']
})
export class InvTransactionHistoryComponent implements OnInit {

  invTransactions: InvestmentTransaction[];

  constructor(private investmentDetails: InvestmentsService) { }

  ngOnInit() {
    this.invTransactions = this.investmentDetails.getInvestmentsInformation().Accounts[0].Transactions;
  }

  getIncome() {
    let sum = 0;
    this.invTransactions.forEach((t) => {
      if (t.Type === "sell") {
        sum += t.Amount*(-t.Quantity);
      }
    });
    return sum;
  }

  getExpense() {
    let sum = 0;
    this.invTransactions.forEach((t) => {
      if (t.Type === "buy") {
        sum += t.Amount*t.Quantity;
      }
    });
    return sum;
  }

  getProfit(){
    return this.getIncome() - (-this.getExpense());
  }

  getProfitColor(){
    let profit = this.getProfit();

    if (profit >= 0){
      return "#28df99"
    }
    else {
      return "#ffa500"
    }
  }

  getTransactionArrow(transaction: InvestmentTransaction) {
    if (transaction.Type == "sell") {
      return "fa-long-arrow-down";
    } else {
      return "fa-long-arrow-up";
    }
  }

  getPlusOrMinus(transaction: InvestmentTransaction) {
    if (transaction.Type == "sell") {
      return "fa-minus-circle";
    } else {
      return "fa-plus-circle";
    }
  }

  getAmount(transaction: InvestmentTransaction) {
    return transaction.Amount * -transaction.Quantity;
  }

}
