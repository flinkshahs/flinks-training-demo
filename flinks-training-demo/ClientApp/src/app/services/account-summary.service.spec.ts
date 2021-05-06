import { TestBed } from '@angular/core/testing';

import { AccountSummaryService } from './account-summary.service';

describe('AccountSummaryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AccountSummaryService = TestBed.get(AccountSummaryService);
    expect(service).toBeTruthy();
  });
});
