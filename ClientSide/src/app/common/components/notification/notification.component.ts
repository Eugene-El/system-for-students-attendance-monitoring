import { Component, OnInit } from '@angular/core';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { NotificationModel } from '../../models/notificationModel';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {

  constructor(
    private notificationService: NotificationService
  ) { }

  ngOnInit() {
    this.notificationService.onNotification.subscribe((notification: NotificationModel) => {
      this.page.visible = true;
      this.page.message = notification.message;
      
      this.page.isSuccess = notification.type == "success";
      this.page.isError = notification.type == "error";

      if (notification.durationMs < 3000)
        notification.durationMs = 3000;

      this.page.currentStep = 0;
      this.page.durration = notification.durationMs;
      
      this.methods.progress();
    })
  }

  page = {
    visible: false as boolean,
    message: "" as string,

    isSuccess: false as boolean,
    isError: false as boolean,

    step: 100 as number,
    currentStep: 0 as number,
    durration: 0 as number,
    mouseHovered: false as boolean
  }

  methods = {
    progress: () => {
      if (this.page.currentStep >= this.page.durration)
      {
        this.page.visible = false;
        return;
      }
      
      if (!this.page.mouseHovered)
        this.page.currentStep += this.page.step;
      let progress = document.getElementById("app-notification-progress-bar");
      if (progress)
      {
        progress.style.width = (this.page.currentStep / this.page.durration * 100) + "%";
      }

      setTimeout(this.methods.progress, this.page.step);
    },
    mouseEnter: () => {
      this.page.mouseHovered = true;
    },
    mouseLeave: () => {
      this.page.mouseHovered = false;
    }
  }

}
