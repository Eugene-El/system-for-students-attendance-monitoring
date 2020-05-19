import { Component, OnInit } from '@angular/core';
import { ConfigurationService } from './services/configuration.service';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { ConfigurationModel } from './models/configurationModel';
import { NotificationService } from 'src/app/services/notification-service/notification.service';

@Component({
  selector: 'app-configuration',
  templateUrl: './configuration.component.html',
  styleUrls: ['./configuration.component.css']
})
export class ConfigurationComponent implements OnInit {

  constructor(
    private loadingService: LoadingService,
    private configurationService: ConfigurationService,
    private notificationService: NotificationService
  ) { }

  ngOnInit() {
    this.loadingService.endLoading();
    this.methods.getConfiguration();
  }

  page = {
    configuration: null as ConfigurationModel
  }

  methods = {
    getConfiguration: () => {
      this.loadingService.addLoading();
      this.configurationService.get().then((configuration) => {
        this.page.configuration = configuration;
        this.loadingService.endLoading();
      }).catch((error) => {
        this.notificationService.processError(error);
        this.loadingService.endLoading();
      });
    }, 
    saveConfiguration: () => {
      this.page.configuration.notificationAndSyncConfiguration.dataSyncPeriodHours = Math.round(this.page.configuration.notificationAndSyncConfiguration.dataSyncPeriodHours);
      this.page.configuration.notificationAndSyncConfiguration.notificationsPeriodHours = Math.round(this.page.configuration.notificationAndSyncConfiguration.notificationsPeriodHours);
      this.loadingService.addLoading();
      this.configurationService.save(this.page.configuration).then(() => {
        this.notificationService.successfullySaved();
        this.loadingService.endLoading();
        this.methods.getConfiguration();
      }).catch((error) => {
        this.notificationService.processError(error);
        this.loadingService.endLoading();
      });
    }
  }
}
