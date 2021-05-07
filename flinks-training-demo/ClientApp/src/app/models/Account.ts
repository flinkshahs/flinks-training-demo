import { Transaction } from "./Transaction"
import { Holder } from "./Holder"
import { Balance } from "./Balance"

export class Account {
    Transactions:Transaction[];
    TransitNumber:string;
    InstitutionNumber:string;
    OverdraftLimit: number;
    Title: string;
    AccountNumber:string;
    Balance:Balance;
    Category: string;
    Type: string;
    Currency: string;
    Holder: Holder;
    Id: string;
}