import { Component, OnInit } from "@angular/core";
import { Transaction } from "../models/Transaction";
import { Holder } from "../models/Holder"
import { AccountDetailsService } from "../services/account-details.service";

@Component({
  selector: "app-account-details",
  templateUrl: "./account-details.component.html",
  styleUrls: ["./account-details.component.css"],
})

export class AccountDetailsComponent implements OnInit {
  transactions: Transaction[];
  holder: Holder;

  constructor(private transaction_data: AccountDetailsService) {}

  ngOnInit() {
    this.transactions = this.transaction_data.getTransactions();
     this.holder = {
      Name: "John Doe",
      Address: {
        CivicAddress: "1275 avenue des Canadiens-de-Montréal",
        City: "Montréal",
        Province: "QC",
        PostalCode: "H3B 5E8",
        POBox: null,
        Country: "CA",
      },
      Email: "johndoe@flinks.com",
      PhoneNumber: "(514) 333-7777",
    };
  }
}
