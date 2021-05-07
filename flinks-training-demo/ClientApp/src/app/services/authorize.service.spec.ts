import { TestBed } from '@angular/core/testing';

import { AuthorizeService } from './authorize.service';

describe('AuthorizeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AuthorizeService = TestBed.get(AuthorizeService);
    expect(service).toBeTruthy();
  });
});
