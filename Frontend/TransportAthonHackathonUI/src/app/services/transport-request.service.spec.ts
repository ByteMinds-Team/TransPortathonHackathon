import { TestBed } from '@angular/core/testing';

import { TransportRequestService } from './transport-request.service';

describe('TransportRequestService', () => {
  let service: TransportRequestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TransportRequestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
