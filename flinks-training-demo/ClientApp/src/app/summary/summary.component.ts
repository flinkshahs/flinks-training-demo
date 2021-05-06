import { Component, Input } from '@angular/core';
import { A } from '../mockAccount'; //constant Account for testing
import {Account} from './summary.service';


@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
})

//declaring input variables from API calls
export class SummaryComponent {
  selectedAccount: Account;
 constructor(){}

  
//tester method to simulate an API that would change these
  changeInfo(){
    this.selectedAccount = A;
  }


}
