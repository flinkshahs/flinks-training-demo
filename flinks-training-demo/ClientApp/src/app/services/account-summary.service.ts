import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class AccountSummaryService {
  constructor(private http: HttpClient) {}

  //interface of Account
  getAccountSummary() {
    return {
      transitNumber: 35032,
      institutionNumber: 203,
      availableBalance: 330,
      limitBalance: 600,
      holder: "Flinks Person",
      overdraftLimit: 80,
      title: "Chequing CAD",
      category: "Chequing",
      accountNumber: "1111000",
      currentBalance: 49993.96,
      type: "Chequing",
      currency: "CAD",
      loginId: "ae1dac72-70da-4626-ded8-08d682e1ff4",
    };
  }
}
