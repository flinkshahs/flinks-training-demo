import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  @Input() transitNumber?: number;
  @Input() institutionNumber?: number;
  @Input() overdraftLimit?: number;
  @Input() title?: string;
  @Input() accountNumber?: string;
  @Input() availableBalance?: number;
  @Input() currentBalance?: number;
  @Input() limitBalance?: number;
  @Input() category?: string;
  @Input() type?: string;
  @Input() currency?: string;
  @Input() holder?: string;
  @Input() loginId?: string;

  changeInfo(){
    
    this.transitNumber = 18834;
    this.institutionNumber = 203;
    this.availableBalance = 330;
    this.limitBalance = 600;
    this.holder = "Flinks Person";
    
    this.overdraftLimit = 0;
    this.title = "Chequing CAD";
    this.accountNumber = "1111000";
    this.currentBalance = 49993.96;
    this.type = "Chequing";
    this.currency = "CAD";
    this.loginId = "ae1dac72-70da-4626-ded8-08d682e1ff4";
    
  }

  isButtonVisible = false;


}
