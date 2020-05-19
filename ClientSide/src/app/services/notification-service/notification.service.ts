import { Injectable, Output, EventEmitter } from '@angular/core';
import { NotificationModel } from 'src/app/common/models/notificationModel';
import { TranslateService } from '@ngx-translate/core';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  @Output() onNotification = new EventEmitter<NotificationModel>();

  private displayTime = 4000;
  private errorDisplayTime = 6000;

  constructor(private translateService: TranslateService) { }

  public success(message: string) {
    this.onNotification.emit(new NotificationModel(this.translateService.instant(message), "success", this.displayTime));
  }

  public info(message: string) {
    this.onNotification.emit(new NotificationModel(this.translateService.instant(message), "info", this.displayTime));
  }

  public error(message: string) {
    this.onNotification.emit(new NotificationModel(this.translateService.instant(message), "error", this.errorDisplayTime));
  }

  public fillAllFields() {
    this.onNotification.emit(new NotificationModel(this.translateService.instant("COMMON.PLEASE_FILL_ALL_FIELDS"), "info", this.displayTime));
  }

  public successfullySaved() {
    this.onNotification.emit(new NotificationModel(this.translateService.instant("COMMON.SUCESSFULLY_SAVED"), "success", this.displayTime));
  }

  public processError(error: HttpErrorResponse) {
    console.log(error);
    let errorString = "";
    if (typeof(error.error) == "string")
      errorString = error.error;
    else if (error.error != null && typeof(error.error) == "object" && error.error.Message != null && typeof(error.error.Message) == "string")
      errorString = error.error.Message;
    else
      errorString = error.message;
    this.error(errorString);
  }

}
