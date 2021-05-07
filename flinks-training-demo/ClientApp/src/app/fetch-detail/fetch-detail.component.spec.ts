import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FetchDetailComponent } from './fetch-detail.component';

describe('FetchDetailComponent', () => {
  let component: FetchDetailComponent;
  let fixture: ComponentFixture<FetchDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FetchDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FetchDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
