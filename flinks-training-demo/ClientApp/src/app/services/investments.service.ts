import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class InvestmentsService {

  constructor(private http: HttpClient) { }

  getInvestmentsInformation(){
    return {
      Accounts: [
          {
              "Id": "500efd20-8643-49f1-b61c-08d845f8e6df",
              "FullAccountId": "non-registered-123456:cash:cad",
              "AccountId": "123456",
              "Name": "Random Investment",
              "Type": "cash",
              "Registered": false,
              "Currency": "CAD",
              "Cash": 100.00,
              "AccountValue": 16.32,
              "CurrencyValue": 16.32,
              "Positions": [
                  {
                      "Id": "b2ee340f-051b-4fd9-db14-08d845f8e6e0",
                      "Category": "Canadian Stocks",
                      "Class": "equity",
                      "BookValue": 600.00,
                      "Quantity": 120,
                      "MarketValue": 549.60,
                      "GainAmount": -50.40,
                      "GainCurrencyAmount": -50.40,
                      "GainPercent": -0.08,
                      "Currency": "CAD",
                      "SecurityId": "de7539cf-6814-4f24-993b-20f54b860b6d"
                  },
                  {
                      "Id": "cc71388b-2055-41a6-db15-08d845f8e6e0",
                      "Category": "US Stocks",
                      "Class": "equity",
                      "BookValue": 600.00,
                      "Quantity": 6,
                      "MarketValue": 599.94,
                      "GainAmount": -0.06,
                      "GainCurrencyAmount": -0.06,
                      "GainPercent": 0.00,
                      "Currency": "CAD",
                      "SecurityId": "1f4ccbe8-2eae-43f4-9d94-a054f90e5197"
                  },
                  {
                      "Id": "ca2ba9c4-f133-47f0-db16-08d845f8e6e0",
                      "Category": "US Stocks",
                      "Class": "equity",
                      "BookValue": 600.00,
                      "Quantity": 1,
                      "MarketValue": 555.87,
                      "GainAmount": -44.13,
                      "GainCurrencyAmount": -44.13,
                      "GainPercent": -0.07,
                      "Currency": "CAD",
                      "SecurityId": "74d8129e-52ab-4f0b-9b9e-eaa5e047a187"
                  }
              ],
              Transactions: [
                  {
                      Id: "0e76c626-5771-4277-6c06-08d845f8e6e6",
                      Date: "2020-07-31",
                      Type: "sell",
                      Description: "Sell 3 AAPL on last day of the month",
                      Quantity: -3,
                      Fee: 0.00,
                      Amount: 400.00,
                      SecurityId: "1f4ccbe8-2eae-43f4-9d94-a054f90e5197"
                  },
                  {
                      Id: "366f3864-01d9-45d2-6c07-08d845f8e6e6",
                      Date: "2020-08-01",
                      Type: "buy",
                      Description: "Buy 3 AAPL on first day of the month",
                      Quantity: 3,
                      Fee: 0.00,
                      Amount: -300.00,
                      SecurityId: "1f4ccbe8-2eae-43f4-9d94-a054f90e5197"
                  },
                  {
                      Id: "307afa6b-4aad-4f76-52b0-08d85f231bb7",
                      Date: "2020-08-31",
                      Type: "sell",
                      Description: "Sell 3 AAPL on last day of the month",
                      Quantity: -3,
                      Fee: 0.00,
                      Amount: 400.00,
                      SecurityId: "fcc50cd6-3de8-4bae-913f-e9d313963c70"
                  },
                  {
                      Id: "bdbf333e-b08e-4998-52b1-08d85f231bb7",
                      Date: "2020-09-01",
                      Type: "buy",
                      Description: "Buy 3 AAPL on first day of the month",
                      Quantity: 3,
                      Fee: 0.00,
                      Amount: -300.00,
                      SecurityId: "fcc50cd6-3de8-4bae-913f-e9d313963c70"
                  }
              ]
          },
          {
              "Id": "8f43bf1d-6bfe-492f-b61d-08d845f8e6df",
               "FullAccountId": "non-registered-654321:cash:usd",
              "AccountId": "654321",
              "Name": "Uncle Sam",
              "Type": "cash",
              "Registered": false,
              "Currency": "USD",
              "Cash": 100.00,
              "AccountValue": 132.93,
              "CurrencyValue": 100.00,
              "Positions": null,
              "Transactions": null
          }
      ],
      "Securities": [
          {
              "Id": "6b48f77c-1ad1-477f-9cbd-dedbe089a8e3",
              "Name": "BlackBerry Ltd",
              "Symbol": "BB.DEMO",
              "Type": "equity",
              "Price": 4.34,
              "Date": "2020-09-21T22:07:24.2480000Z",
              "Aliases": [
                  "BlackBerry Ltd"
              ],
              "Currency": "CAD",
              "CUSIP": "09228F",
              "ISIN": null,
          },
          {
              "Id": "fcc50cd6-3de8-4bae-913f-e9d313963c70",
              "Name": "Apple Inc.",
              "Symbol": "AAPL.DEMO",
              "Type": "equity",
              "Price": 62.23,
              "Date": "2020-09-21T22:07:24.2370000Z",
              "Aliases": [
                  "Apple Inc."
              ],
              "Currency": "USD",
              "CUSIP": "037833100",
              "ISIN": "US0378331005"
          },
          {
              "Id": "f6ab80f8-af7a-4b8e-a8bc-6639d5939edc",
              "Name": "Google Inc",
              "Symbol": "GOOGL.DEMO",
              "Type": "equity",
              "Price": 336.37,
              "Date": "2020-09-21T22:07:24.2240000Z",
              "Aliases": [
                  "Google Inc"
              ],
              "Currency": "USD",
              "CUSIP": "02079K305",
              "ISIN": null,
          }
      ],
      "Value": 149.25,
      "Cash": 232.93,
      "HttpStatusCode": 200,
      "Login": {
          "Username": "wealth@flinks.com",
          "IsScheduledRefresh": false,
          "LastRefresh": "2020-09-22T18:12:45.9357137",
          "Type": null,
          "Id": "4500022b-69be-4481-beee-08d845f8da3d"
      },
      "Institution": {
          "Id": 999999999,
          "Name": "FlinksInvestment"
      },
      "RequestId": "e705e4d8-e7e8-4e17-ab5c-802fd6a52188"
  }
  }
}
