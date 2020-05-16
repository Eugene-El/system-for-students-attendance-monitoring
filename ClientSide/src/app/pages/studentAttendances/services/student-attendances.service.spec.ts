/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { StudentAttendancesService } from './student-attendances.service';

describe('Service: StudentAttendances', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [StudentAttendancesService]
    });
  });

  it('should ...', inject([StudentAttendancesService], (service: StudentAttendancesService) => {
    expect(service).toBeTruthy();
  }));
});
