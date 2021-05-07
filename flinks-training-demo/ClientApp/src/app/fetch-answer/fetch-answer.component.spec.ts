import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FetchAnswerComponent } from './fetch-answer.component';

describe('FetchAnswerComponent', () => {
  let component: FetchAnswerComponent;
  let fixture: ComponentFixture<FetchAnswerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FetchAnswerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FetchAnswerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
