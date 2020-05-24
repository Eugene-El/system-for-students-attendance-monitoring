import { Component, OnInit, ViewChild } from '@angular/core';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { StudentGridModel } from '../models/studentGridModel';
import { MatPaginator } from '@angular/material/paginator';
import { StudentsService } from '../services/students.service';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { Sort } from '@angular/material/sort';
import { AuthorizationService } from 'src/app/services/authorization-service/authorization.service';

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
    private router: Router,
    private authorizationService: AuthorizationService
    ) { }

  ngOnInit() {
    this.loadingService.endLoading();
    this.methods.getStudents();
    this.translateService.onLangChange.subscribe(() => { this.methods.getStudents(); });
    this.page.showStudentEdit = this.authorizationService.isWorker();
  }

  page = {
    showStudentEdit: false
  }

  dataSources = {
    students: new Array<StudentGridModel>(),
    sortedStudents: new MatTableDataSource<StudentGridModel>(),
    learningForms: this.studentsService.getLearningFormsDictionary() ,
    studentLanguages: this.studentsService.getStudentLanguagesDictionary()
  }

  methods = {
    getStudents: () => {
      this.loadingService.addLoading();
      this.studentsService.getAllForGrid().then((students) => {
        this.dataSources.students = students;
        this.dataSources.sortedStudents = new MatTableDataSource(students);
        this.dataSources.sortedStudents.paginator = this.paginator;
        this.loadingService.endLoading();
      }).catch((error) => {
        this.notificationService.processError(error);
        this.loadingService.endLoading();
      });
    },
    applyFilter: (e) => {
      let filterValue = (e.target as HTMLInputElement).value;
      this.dataSources.sortedStudents.filter = filterValue.trim().toLowerCase();

      if (this.dataSources.sortedStudents.paginator)
        this.dataSources.sortedStudents.paginator.firstPage();
    },
    sortData: (sort: Sort) => {

      let data = this.dataSources.students.slice(); // create new array insatnce
      if (sort.direction === '') {
        let filter = this.dataSources.sortedStudents.filter;
        this.dataSources.sortedStudents = new MatTableDataSource(data);
        this.dataSources.sortedStudents.paginator = this.paginator;
        this.dataSources.sortedStudents.filter = filter;
        return;
      }
  
      let sortedData = data.sort((a, b) => {
        let isAsc = sort.direction === 'asc';
        switch (sort.active) {
          case 'code': return this.methods.compare(a.code, b.code, isAsc);
          case 'fullName': return this.methods.compare(a.fullName, b.fullName, isAsc);
          case 'facultyTitle': return this.methods.compare(a.facultyTitle, b.facultyTitle, isAsc);
          case 'studyProgrammeTitle': return this.methods.compare(a.studyProgrammeTitle, b.studyProgrammeTitle, isAsc);
          case 'learningForm': return this.methods.compare(a.learningForm, b.learningForm, isAsc);
          case 'studentLanguage': return this.methods.compare(a.studentLanguage, b.studentLanguage, isAsc);
          default: return 0;
        }
      });

      let filter = this.dataSources.sortedStudents.filter;
      this.dataSources.sortedStudents = new MatTableDataSource(sortedData);
      this.dataSources.sortedStudents.paginator = this.paginator;
      this.dataSources.sortedStudents.filter = filter;
    },
    compare: (a: number | string, b: number | string, isAsc: boolean) => {
      return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
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
