/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { NotificationHistoryService } from './notificationHistory.service';

describe('Service: NotificationHistory', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NotificationHistoryService]
    });
  });

  it('should ...', inject([NotificationHistoryService], (service: NotificationHistoryService) => {
    expect(service).toBeTruthy();
  }));
});
