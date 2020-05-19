import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { SubjectGridModel } from '../models/subjectGridModel';
import { SubjectsService } from '../services/subjects.service';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { MatPaginator } from '@angular/material/paginator';
import { NotificationService } from 'src/app/services/notification-service/notification.service';

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
    subjects: new MatTableDataSource<SubjectGridModel>()
  }

  methods = {
    getSubjects: () => {
      this.loadingService.addLoading();
      this.subjectsService.getAllForGrid().then((subjects) => {
        this.dataSources.subjects = new MatTableDataSource(subjects);
        this.dataSources.subjects.paginator = this.paginator;
        this.loadingService.endLoading();
      }).catch((error) => {
        this.notificationService.processError(error);
        this.loadingService.endLoading();
      });
    },
    applyFilter: (e) => {
      let filterValue = (e.target as HTMLInputElement).value;
      this.dataSources.subjects.filter = filterValue.trim().toLowerCase();

      if (this.dataSources.subjects.paginator)
        this.dataSources.subjects.paginator.firstPage();
    },
    addNew: () => {
      this.router.navigate(['subjects', 0]);
    },
    edit: (id) => {
      this.router.navigate(['subjects', id]);
    }
  }
}
