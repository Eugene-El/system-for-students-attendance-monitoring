/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { NotificationDataService } from './notificationData.service';

describe('Service: NotificationData', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NotificationDataService]
    });
  });

  it('should ...', inject([NotificationDataService], (service: NotificationDataService) => {
    expect(service).toBeTruthy();
  }));
});
