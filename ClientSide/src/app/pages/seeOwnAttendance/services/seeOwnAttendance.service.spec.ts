/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SeeOwnAttendanceService } from './seeOwnAttendance.service';

describe('Service: SeeOwnAttendance', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SeeOwnAttendanceService]
    });
  });

  it('should ...', inject([SeeOwnAttendanceService], (service: SeeOwnAttendanceService) => {
    expect(service).toBeTruthy();
  }));
});
