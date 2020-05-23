/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AuthorizationDataService } from './authorization-data.service';

describe('Service: AuthorizationData', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthorizationDataService]
    });
  });

  it('should ...', inject([AuthorizationDataService], (service: AuthorizationDataService) => {
    expect(service).toBeTruthy();
  }));
});
