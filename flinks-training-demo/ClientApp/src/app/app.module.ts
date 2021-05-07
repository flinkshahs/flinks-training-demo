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
import { SecurityQuestionComponent } from './security-question/security-question.component';
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
    SecurityQuestionComponent,
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
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'security-question', component: SecurityQuestionComponent },
      { path: 'fetch-answer', component: FetchAnswerComponent },
      { path: 'fetch-summary', component: FetchSummaryComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
