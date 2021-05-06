import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class AccountDetailsService {
  constructor(private http: HttpClient) {}

  getAccountInformation() {
    return {
      Transactions: this.getTransactions(),
      TransitNumber: "77777",
      InstitutionNumber: "777",
      OverdraftLimit: 0,
      Title: "Chequing CAD",
      AccountNumber: "1111000",
      Balance: {
        Available: null,
        Current: 49993.96,
        Limit: null,
      },
      Category: "Operations",
      Type: "Chequing",
      Currency: "CAD",
      Holder: this.getHolderInformation(),
      Id: "ae1dac72-70da-4626-fed8-08d682e1ff4a",
    };
  }
  getHolderInformation() {
    return {
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

      Balance: {
        Available: null,
        Current: 49993.96,
        Limit: null,
      },
    };
  }
  getTransactions() {
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
