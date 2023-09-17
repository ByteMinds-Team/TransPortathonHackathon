import { TestBed } from '@angular/core/testing';

import { TransportTypeService } from './transport-type.service';

describe('TransportTypeService', () => {
  let service: TransportTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TransportTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
