import { Component, OnInit } from '@angular/core';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import * as moment from 'moment';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { HomeService } from './services/home.service';
import { HomeDataModel } from './models/homeDataModel';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(
    private loadingService: LoadingService,
    private notificationService: NotificationService,
    private translateService: TranslateService,
    private homeService: HomeService
  ) { }

  ngOnInit() {
    this.loadingService.endLoading();
    this.methods.updateCurrentTime();
    this.methods.getData();
  }

  page = {
    currentTime: "" as string,
    currentOnline: 0 as number
  }

  options = {
    colors: [
      {
        backgroundColor: ['rgba(0,255,0,0.3)', 'rgba(255,0,0,0.3)'],
      }
    ]
  }

  dataSorces = {
    chartData: [],
    chartTitles: [],
    chartLabels: []
  }

  methods = {
    getData: () => {
      this.loadingService.addLoading();
      this.homeService.getFacultyStatistics()
        .then((homeDataModel) => {
          this.page.currentOnline = homeDataModel.currentOnline;

          this.dataSorces.chartData = [];
          this.dataSorces.chartTitles = [];
          this.dataSorces.chartLabels = [
            this.translateService.instant("HOME.VISITED"),
            this.translateService.instant("HOME.NOT_VISITED"),
          ];
          homeDataModel.facultyStatistics.forEach(st => {
            this.dataSorces.chartData.push([st.attendancePercent, 100 - st.attendancePercent]);
            this.dataSorces.chartTitles.push(st.title);
          });

          this.loadingService.endLoading();
        })
        .catch((error) => {
          this.notificationService.processError(error);
          this.loadingService.endLoading();
        });
    },
    updateCurrentTime: () => {
      let date = new Date();
      this.page.currentTime = moment().format("DD.MM.yyyy") + ' '
        + this.methods.addWhitespace(date.getHours()) + ':'
        + this.methods.addWhitespace(date.getMinutes());
      setTimeout(() => {
        this.methods.updateCurrentTime();
      }, 1000);
    },
    addWhitespace: (number: number) : string => {
      return (number < 10 ? '0': '') + number;
    }
  }
}
