import { Component, Input, OnInit } from '@angular/core';
import {Account} from '../models/AccountSummary';
import { AccountSummaryService } from "../services/account-summary.service";

@Component({
  selector: 'app-account-summary',
  templateUrl: './account-summary.component.html',
  styleUrls: ['./account-summary.component.css']
})

//declaring input variables from API calls
export class AccountSummaryComponent implements OnInit{
  selectedAccount: Account;

 constructor(private accountSummaryService : AccountSummaryService){}

  ngOnInit(): void {
    this.selectedAccount = this.accountSummaryService.getAccountSummary();
  }

}

