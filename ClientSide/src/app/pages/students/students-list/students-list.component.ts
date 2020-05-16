import { Component, OnInit, ViewChild } from '@angular/core';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { StudentGridModel } from '../models/studentGridModel';
import { MatPaginator } from '@angular/material/paginator';
import { StudentsService } from '../services/students.service';
import { NotificationService } from 'src/app/services/notification-service/notification.service';

@Component({
  selector: 'app-students-list',
  templateUrl: './students-list.component.html',
  styleUrls: ['./students-list.component.css']
})
export class StudentsListComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  
  constructor(
    private studentsService: StudentsService,
    private loadingService: LoadingService,
    private translateService: TranslateService,
    private notificationService: NotificationService,
    private router: Router
    ) { }

  ngOnInit() {
    this.loadingService.endLoading();
    this.methods.getStudents();
    this.translateService.onLangChange.subscribe(() => { this.methods.getStudents(); });
  }

  dataSources = {
    students: new MatTableDataSource<StudentGridModel>(),
    learningForms: this.studentsService.getLearningFormsDictionary() ,
    studentLanguages: this.studentsService.getStudentLanguagesDictionary()
  }

  methods = {
    getStudents: () => {
      this.loadingService.addLoading();
      this.studentsService.getAllForGrid().then((students) => {
        this.dataSources.students = new MatTableDataSource(students);
        this.dataSources.students.paginator = this.paginator;
        this.loadingService.endLoading();
      }).catch((error) => {
        this.notificationService.processError(error);
        this.loadingService.endLoading();
      });
    },
    applyFilter: (e) => {
      let filterValue = (e.target as HTMLInputElement).value;
      this.dataSources.students.filter = filterValue.trim().toLowerCase();

      if (this.dataSources.students.paginator)
        this.dataSources.students.paginator.firstPage();
    },
    addNew: () => {
      this.router.navigate(['students', 0]);
    },
    edit: (id) => {
      this.router.navigate(['students', id]);
    },
    goToAttendance: (id) => {
      this.router.navigate(['students', 'attendances', id]);
    }
  }

}
