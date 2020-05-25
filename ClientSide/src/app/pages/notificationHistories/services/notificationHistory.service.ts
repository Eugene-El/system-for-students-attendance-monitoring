import { Injectable } from '@angular/core';
import { DataService } from 'src/app/services/data-service/data.service';
import { NotificationHistoryModel } from '../models/notificationHistoryModel';
import { SelectModel } from 'src/app/common/models/selectModel';

@Injectable({
  providedIn: 'root'
})
export class NotificationHistoryService {

  constructor(private dataService: DataService) { }

  private paths = {
    getAll: 'api/NotificationHistories/GetAll/',
  }

  public getAll() : Promise<Array<NotificationHistoryModel>> {
    return this.dataService.get(this.paths.getAll);
  }

  public getNotificationStatusesDictionary() {
    return this.makeDictionary(this.getNotificationStatuses());
  }
  public getNotificationStatuses() {
    return [
      new SelectModel(1, "NOTIFICATION_HISTORY.STATUSES.SUCCESS"),
      new SelectModel(2, "NOTIFICATION_HISTORY.STATUSES.ERROR"),
    ];
  }

  private makeDictionary(values: Array<SelectModel>) {
    let dictionary = {};
    values.forEach(val => dictionary[val.id] = val.title);
    return dictionary;
  }
}
