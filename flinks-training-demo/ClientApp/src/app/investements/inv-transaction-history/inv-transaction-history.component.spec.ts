import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InvTransactionHistoryComponent } from './inv-transaction-history.component';

describe('InvTransactionHistoryComponent', () => {
  let component: InvTransactionHistoryComponent;
  let fixture: ComponentFixture<InvTransactionHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InvTransactionHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InvTransactionHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
