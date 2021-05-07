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
import { AccountSummaryComponent } from './account-summary/account-summary.component';
import { LoginComponent } from './login/login.component';
import { FetchAnswerComponent } from './fetch-answer/fetch-answer.component';
import { FetchSummaryComponent } from './fetch-summary/fetch-summary.component';
import { FetchDetailComponent } from './fetch-detail/fetch-detail.component';

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
    InvTransactionHistoryComponent,
    AccountSummaryComponent,
    LoginComponent,
    FetchAnswerComponent,
    FetchSummaryComponent,
    FetchDetailComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'get-account-details', component: CounterComponent },
      { path: 'log-out', component: FetchDataComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent},
      { path: 'fetch-answer', component: FetchAnswerComponent },
      { path: 'fetch-summary', component: FetchSummaryComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
