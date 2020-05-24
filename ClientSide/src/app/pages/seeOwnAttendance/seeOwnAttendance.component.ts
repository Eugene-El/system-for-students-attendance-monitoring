import { Component, OnInit } from '@angular/core';
import { AttendanceFilterModel } from './models/attendanceFilterModel';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { SeeOwnAttendanceService } from './services/seeOwnAttendance.service';
import { AttendanceStatisticsModel } from './models/attendanceStatisticsModel';
import { SelectModel } from 'src/app/common/models/selectModel';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-seeOwnAttendance',
  templateUrl: './seeOwnAttendance.component.html',
  styleUrls: ['./seeOwnAttendance.component.css']
})
export class SeeOwnAttendanceComponent implements OnInit {

  constructor(
    private loadingService: LoadingService,
    private notificationService: NotificationService,
    private seeOwnAttendanceService: SeeOwnAttendanceService,
    private translateService: TranslateService
  ) { }

  ngOnInit() {
    this.loadingService.endLoading();
    this.page.to = new Date();
    this.page.from = new Date(new Date().getTime() - 1000*60*60*24*30); // - 1 month
    this.methods.fillLabels();
    this.methods.filter();
    this.translateService.onLangChange.subscribe(() => {
      this.methods.fillLabels();
      this.methods.filter();
    });
  }

  page = {
    from: null,
    to: null,
    statistics: null as AttendanceStatisticsModel
  }

  options = {
    colors: [
      {
        backgroundColor: ['rgba(0,255,0,0.3)', 'rgba(255,0,0,0.3)'],
      }
    ],
    tsiColor: [
      {
        backgroundColor: ['rgba(20, 71, 116, 0.5)'],
      }
    ],
    labels: [],
    lineChartOptions: {
      scales : {
        yAxes: [{
          ticks: {
          beginAtZero: true,
              stepValue: 10,
              steps: 10,
            max : 100,
          }
        }]
      }
    }
  }

  dataSources = {
    globalStatData: [],
    globalStatLabels: [],
    subjects: [],
    subjectStatData: [],
    subjectStatLabels: [],
  }

  methods = {
    filter: () => {
      let filter = new AttendanceFilterModel(
        this.methods.fixDate(this.page.from),
        this.methods.fixDate(this.page.to),
      );
      
      this.loadingService.addLoading();
      this.seeOwnAttendanceService.getAllForGrid(filter)
        .then((statistics) => {
          this.loadingService.endLoading();
          this.page.statistics = statistics;
          this.dataSources.globalStatData = statistics.globalStatistics.map(g => g.attendanceProcent);
          this.dataSources.globalStatLabels = statistics.globalStatistics.map(g => g.date);
          this.dataSources.subjects = statistics.subjectStatistics.map(s => new SelectModel(s.subjectId, s.title));
        })
        .catch((error) => {
          this.notificationService.processError(error);
          this.loadingService.endLoading();
        })
    },
    fixDate: (date: any): Date => {
      if (date.toDate)
      {
        date = date.toDate();
      }
      return new Date(Date.UTC(
        date.getFullYear(),
        date.getMonth(),
        date.getDate()));
    },
    fillLabels: () => {
      this.options.labels = [
        this.translateService.instant("SEE_OWN_ATTENDANCE.VISITED"),
        this.translateService.instant("SEE_OWN_ATTENDANCE.NOT_VISITED")
      ];
    },
    subjectSelected: (id) => {
      let data = this.page.statistics.subjectStatistics.find(s => s.subjectId == id);
      if (data) {
        this.dataSources.subjectStatData = data.dayStatistics.map(g => g.attendanceProcent);
        this.dataSources.subjectStatLabels = data.dayStatistics.map(g => g.date);
      }
    }
  }
}
