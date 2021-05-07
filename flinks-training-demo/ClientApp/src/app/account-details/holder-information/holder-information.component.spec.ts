import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HolderInformationComponent } from './holder-information.component';

describe('HolderInformationComponent', () => {
  let component: HolderInformationComponent;
  let fixture: ComponentFixture<HolderInformationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HolderInformationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HolderInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
