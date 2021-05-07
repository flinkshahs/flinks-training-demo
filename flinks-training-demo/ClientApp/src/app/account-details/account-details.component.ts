import { Component, OnInit } from "@angular/core";
import { Transaction } from "../models/Transaction";
import { Holder } from "../models/Holder";
import { Account } from "../models/Account";
import { AccountDetailsService } from "../services/account-details.service";

@Component({
  selector: "app-account-details",
  templateUrl: "./account-details.component.html",
  styleUrls: ["./account-details.component.css"],
})
export class AccountDetailsComponent implements OnInit {
  account: Account;
  transactions: Transaction[];
  holder: Holder;

  constructor(private accountDetails: AccountDetailsService) {}

  
}
