import { Injectable } from '@angular/core';
import { DataService } from 'src/app/services/data-service/data.service';
import { NotificationRuleDataModel } from '../models/notificationRuleDataModel';
import { NotificationRuleModel } from '../models/notificationRuleModel';
import { SelectModel } from 'src/app/common/models/selectModel';

@Injectable({
  providedIn: 'root'
})
export class NotificationDataService {

  constructor(private dataService: DataService) { }

  private paths = {
    getAll: 'api/NotificationRules/GetAll/',
    save: 'api/NotificationRules/Save/',
    delete: 'api/NotificationRules/Delete/'
  }

  public getAll() : Promise<NotificationRuleDataModel> {
    return this.dataService.get(this.paths.getAll);
  }

  public save(notificationRule: NotificationRuleModel): Promise<void> {
    return this.dataService.post(this.paths.save, notificationRule);
  }

  public delete(id: number): Promise<void> {
    return this.dataService.get(this.paths.delete + id.toString());
  }

  public getNotificationMethods() {
    return [
      new SelectModel(1, "NOTIFICATION_RULE.NOTIFICATION_METHODS.EMAIL"),
      new SelectModel(2, "NOTIFICATION_RULE.NOTIFICATION_METHODS.SMS"),
    ];
  }

}
