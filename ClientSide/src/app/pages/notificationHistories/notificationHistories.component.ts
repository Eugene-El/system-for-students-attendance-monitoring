import { Component, OnInit, ViewChild } from '@angular/core';
import { NotificationHistoryModel } from './models/notificationHistoryModel';
import { MatTableDataSource } from '@angular/material/table';
import { NotificationHistoryService } from './services/notificationHistory.service';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { TranslateService } from '@ngx-translate/core';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { MatPaginator } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';

@Component({
  selector: 'app-notificationHistories',
  templateUrl: './notificationHistories.component.html',
  styleUrls: ['./notificationHistories.component.css']
})
export class NotificationHistoriesComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  
  constructor(
    private notificationHistoryService: NotificationHistoryService,
    private loadingService: LoadingService,
    private translateService: TranslateService,
    private notificationService: NotificationService
  ) { }

  ngOnInit() {
    this.loadingService.endLoading();
    this.methods.getNotificationHistories();
  }

  dataSources = {
    notificationHistories: new Array<NotificationHistoryModel>(),
    sortedNotificationHistories: new MatTableDataSource<NotificationHistoryModel>(),

    notificationStatuses: this.notificationHistoryService.getNotificationStatusesDictionary()
  }

  methods = {
    getNotificationHistories: () => {
      this.loadingService.addLoading();
      this.notificationHistoryService.getAll()
      .then((notificationHistories) => {
        this.dataSources.notificationHistories = notificationHistories;
        this.dataSources.sortedNotificationHistories = new MatTableDataSource(notificationHistories);
        this.dataSources.sortedNotificationHistories.paginator = this.paginator;
        this.loadingService.endLoading();
      }).catch((error) => {
        this.notificationService.processError(error);
        this.loadingService.endLoading();
      });
    },
    applyFilter: (e) => {
      let filterValue = (e.target as HTMLInputElement).value;
      this.dataSources.sortedNotificationHistories.filter = filterValue.trim().toLowerCase();

      if (this.dataSources.sortedNotificationHistories.paginator)
        this.dataSources.sortedNotificationHistories.paginator.firstPage();
    },
    sortData: (sort: Sort) => {

      let data = this.dataSources.notificationHistories.slice(); // create new array insatnce
      if (sort.direction === '') {
        
        let filter = this.dataSources.sortedNotificationHistories.filter;
        this.dataSources.sortedNotificationHistories = new MatTableDataSource(data);
        this.dataSources.sortedNotificationHistories.paginator = this.paginator;
        this.dataSources.sortedNotificationHistories.filter = filter;
        return;
      }
  
      let sortedData = data.sort((a, b) => {
        let isAsc = sort.direction === 'asc';
        switch (sort.active) {
          case 'fullName': return this.methods.compare(a.fullName, b.fullName, isAsc);
          case 'sendingTime': return this.methods.dateCompare(a.sendingTime, b.sendingTime, isAsc);
          case 'status': return this.methods.compare(a.status, b.status, isAsc);
          case 'message': return this.methods.compare(a.message, b.message, isAsc);
          case 'errorMessage': return this.methods.compare(a.errorMessage, b.errorMessage, isAsc);
          default: return 0;
        }
      });

      let filter = this.dataSources.sortedNotificationHistories.filter;
      this.dataSources.sortedNotificationHistories = new MatTableDataSource(sortedData);
      this.dataSources.sortedNotificationHistories.paginator = this.paginator;
      this.dataSources.sortedNotificationHistories.filter = filter;
    },
    compare: (a: number | string, b: number | string, isAsc: boolean) => {
      return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
    },
    dateCompare: (a: string, b: string, isAsc: boolean) => {
      let aa = a.split(' ');
      let aaa = aa[0].split('.');
      a = aaa[2] + aaa[1] + aaa[0] + aa[1];
      let bb = b.split(' ');
      let bbb = bb[0].split('.');
      b = bbb[2] + bbb[1] + bbb[0] + bb[1];
      return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
    },
    previewText: (text: string) => {
      return text.substring(0, 50) + (text.length > 50 ? '...' : '');
    }
  }
}
