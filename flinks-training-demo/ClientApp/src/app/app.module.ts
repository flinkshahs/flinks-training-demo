import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { TransactionHistoryComponent } from './account-details/transaction-history/transaction-history.component';
import { HolderInformationComponent } from './account-details/holder-information/holder-information.component';
import { BalanceInformationComponent } from './account-details/balance-information/balance-information.component';
import { InvTransactionHistoryComponent } from './investements/inv-transaction-history/inv-transaction-history.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    AccountDetailsComponent,
    TransactionHistoryComponent,
    HolderInformationComponent,
    BalanceInformationComponent,
    InvTransactionHistoryComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'get-account-details', component: CounterComponent },
      { path: 'log-out', component: FetchDataComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
