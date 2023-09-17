import { TestBed } from '@angular/core/testing';

import { DateGapService } from './date-gap.service';

describe('DateGapService', () => {
  let service: DateGapService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DateGapService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
