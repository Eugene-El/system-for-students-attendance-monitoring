import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { SubjectGridModel } from '../models/subjectGridModel';
import { SubjectsService } from '../services/subjects.service';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { MatPaginator } from '@angular/material/paginator';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { Sort } from '@angular/material/sort';

@Component({
  selector: 'app-subjects-list',
  templateUrl: './subjects-list.component.html',
  styleUrls: ['./subjects-list.component.css']
})
export class SubjectsListComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  
  constructor(
    private subjectsService: SubjectsService,
    private loadingService: LoadingService,
    private translateService: TranslateService,
    private notificationService: NotificationService,
    private router: Router
    ) { }

  ngOnInit() {
    this.loadingService.endLoading();
    this.methods.getSubjects();
    this.translateService.onLangChange.subscribe(() => { this.methods.getSubjects(); });
  }

  dataSources = {
    subjects: new Array<SubjectGridModel>(),
    sortedSubjects: new MatTableDataSource<SubjectGridModel>()
  }

  methods = {
    getSubjects: () => {
      this.loadingService.addLoading();
      this.subjectsService.getAllForGrid().then((subjects) => {
        this.dataSources.subjects = subjects;
        this.dataSources.sortedSubjects = new MatTableDataSource(subjects);
        this.dataSources.sortedSubjects.paginator = this.paginator;
        this.loadingService.endLoading();
      }).catch((error) => {
        this.notificationService.processError(error);
        this.loadingService.endLoading();
      });
    },
    applyFilter: (e) => {
      let filterValue = (e.target as HTMLInputElement).value;
      this.dataSources.sortedSubjects.filter = filterValue.trim().toLowerCase();

      if (this.dataSources.sortedSubjects.paginator)
        this.dataSources.sortedSubjects.paginator.firstPage();
    },
    sortData: (sort: Sort) => {

      let data = this.dataSources.subjects.slice(); // create new array insatnce
      if (sort.direction === '') {
        let filter = this.dataSources.sortedSubjects.filter;
        this.dataSources.sortedSubjects = new MatTableDataSource(data);
        this.dataSources.sortedSubjects.paginator = this.paginator;
        this.dataSources.sortedSubjects.filter = filter;
        return;
      }
  
      let sortedData = data.sort((a, b) => {
        let isAsc = sort.direction === 'asc';
        switch (sort.active) {
          case 'code': return this.methods.compare(a.code, b.code, isAsc);
          case 'title': return this.methods.compare(a.title, b.title, isAsc);
          default: return 0;
        }
      });

      let filter = this.dataSources.sortedSubjects.filter;
      this.dataSources.sortedSubjects = new MatTableDataSource(sortedData);
      this.dataSources.sortedSubjects.paginator = this.paginator;
      this.dataSources.sortedSubjects.filter = filter;

    },
    compare: (a: number | string, b: number | string, isAsc: boolean) => {
      return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
    },
    addNew: () => {
      this.router.navigate(['subjects', 0]);
    },
    edit: (id) => {
      this.router.navigate(['subjects', id]);
    }
  }
}
