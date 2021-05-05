import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountDetailsService {

  constructor(private http:HttpClient) { }

  getTransactions(){
    //
    //This will be replaced by backend POST request response:
    return [
      {
        Date: "2019-04-22",
        Code: null,
        Description: "national money",
        Debit: 12.08,
        Credit: null,
        Balance: 49993.96,
        Id: "633b976e-c713-4b59-9717-3ec407bdde8b",
      },
      {
        Date: "2019-04-21",
        Code: null,
        Description: "TrxChe@Cr12.07",
        Debit: null,
        Credit: 12.07,
        Balance: 50006.04,
        Id: "ac25ab22-2828-4174-9653-23bb8918b7c4",
      },
      {
        Date: "2019-04-20",
        Code: null,
        Description: "TrxChe@De12.06",
        Debit: 12.06,
        Credit: null,
        Balance: 49993.97,
        Id: "1adfde25-832f-4acb-a683-4163bcb4182a",
      },
    ];

  }
}
