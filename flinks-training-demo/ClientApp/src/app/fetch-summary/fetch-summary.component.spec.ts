import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FetchSummaryComponent } from './fetch-summary.component';

describe('FetchSummaryComponent', () => {
  let component: FetchSummaryComponent;
  let fixture: ComponentFixture<FetchSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FetchSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FetchSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
