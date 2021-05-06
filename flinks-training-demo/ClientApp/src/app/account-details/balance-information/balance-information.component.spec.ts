import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BalanceInformationComponent } from './balance-information.component';

describe('BalanceInformationComponent', () => {
  let component: BalanceInformationComponent;
  let fixture: ComponentFixture<BalanceInformationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BalanceInformationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BalanceInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
