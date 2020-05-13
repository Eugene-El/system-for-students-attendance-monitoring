/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FacultiesService } from './faculties.service';

describe('Service: Faculties', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FacultiesService]
    });
  });

  it('should ...', inject([FacultiesService], (service: FacultiesService) => {
    expect(service).toBeTruthy();
  }));
});
