import { TestBed } from '@angular/core/testing';

import { InspectionSvcService } from './inspection-svc.service';

describe('InspectionSvcService', () => {
  let service: InspectionSvcService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InspectionSvcService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
