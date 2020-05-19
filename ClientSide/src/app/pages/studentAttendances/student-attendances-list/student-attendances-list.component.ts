import { Component, OnInit, ViewChild } from '@angular/core';
import { StudentAttendancesService } from '../services/student-attendances.service';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { TranslateService } from '@ngx-translate/core';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { StudentAttendanceGridModel } from '../models/studentAttendanceGridModel';
import { StudentShortModel } from '../models/studentShortModel';
import { Sort } from '@angular/material/sort';

@Component({
  selector: 'app-student-attendances-list',
  templateUrl: './student-attendances-list.component.html',
  styleUrls: ['./student-attendances-list.component.css']
})
export class StudentAttendancesListComponent implements OnInit {
  
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  
  constructor(
    private studentAttendancesService: StudentAttendancesService,
    private loadingService: LoadingService,
    private translateService: TranslateService,
    private notificationService: NotificationService,
    private activatedRoute: ActivatedRoute,
    private router: Router
    ) { }

  ngOnInit() {
    this.loadingService.endLoading();
    this.page.studentId = +this.activatedRoute.snapshot.paramMap.get("studentId");
    this.methods.getStudentAttendances();
    this.translateService.onLangChange.subscribe(() => { this.methods.getStudentAttendances(); });
  }

  page = {
    studentId: 0 as number,
    student: null as StudentShortModel
  }

  dataSources = {
    studentAttendances: new Array<StudentAttendanceGridModel>(),
    sortedStudentAttendances: new MatTableDataSource<StudentAttendanceGridModel>(),
  }

  methods = {
    getStudentAttendances: () => {
      this.loadingService.addLoading();
      this.studentAttendancesService.getAllForGrid(this.page.studentId).then((studentData) => {
        this.dataSources.studentAttendances = studentData.studentAttandances;
        this.dataSources.sortedStudentAttendances = new MatTableDataSource(studentData.studentAttandances);
        this.page.student = studentData.student;
        this.dataSources.sortedStudentAttendances.paginator = this.paginator;
        this.loadingService.endLoading();
      }).catch((error) => {
        this.notificationService.processError(error);
        this.loadingService.endLoading();
      });
    },
    applyFilter: (e) => {
      let filterValue = (e.target as HTMLInputElement).value;
      this.dataSources.sortedStudentAttendances.filter = filterValue.trim().toLowerCase();

      if (this.dataSources.sortedStudentAttendances.paginator)
        this.dataSources.sortedStudentAttendances.paginator.firstPage();
    },
    sortData: (sort: Sort) => {

      let data = this.dataSources.studentAttendances.slice(); // create new array insatnce
      if (sort.direction === '') {
        
        let filter = this.dataSources.sortedStudentAttendances.filter;
        this.dataSources.sortedStudentAttendances = new MatTableDataSource(data);
        this.dataSources.sortedStudentAttendances.paginator = this.paginator;
        this.dataSources.sortedStudentAttendances.filter = filter;
        return;
      }
  
      let sortedData = data.sort((a, b) => {
        let isAsc = sort.direction === 'asc';
        switch (sort.active) {
          case 'subjectTitle': return this.methods.compare(a.subjectTitle, b.subjectTitle, isAsc);
          case 'date': return this.methods.dateCompare(a.date, b.date, isAsc);
          case 'realAttendance': return this.methods.compare(a.realAttendance, b.realAttendance, isAsc);
          case 'necessaryAttendance': return this.methods.compare(a.necessaryAttendance, b.necessaryAttendance, isAsc);
          case 'procent': return this.methods.compare(this.methods.calculateProcent(a), this.methods.calculateProcent(b), isAsc);
          default: return 0;
        }
      });

      let filter = this.dataSources.sortedStudentAttendances.filter;
      this.dataSources.sortedStudentAttendances = new MatTableDataSource(sortedData);
      this.dataSources.sortedStudentAttendances.paginator = this.paginator;
      this.dataSources.sortedStudentAttendances.filter = filter;
    },
    compare: (a: number | string, b: number | string, isAsc: boolean) => {
      return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
    },
    dateCompare: (a: string, b: string, isAsc: boolean) => {
      let aa = a.split('.');
      let bb = b.split('.');
      a = aa[2] + aa[1] + aa[0];
      b = bb[2] + bb[1] + bb[0];
      return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
    },
    calculateProcent: (row) => {
      return Math.round(row.realAttendance / row.necessaryAttendance * 100 * 100) / 100;
    },
    addNew: () => {
      this.router.navigate(['students', 'attendances', this.page.studentId, 0]);
    },
    edit: (id) => {
      this.router.navigate(['students', 'attendances', this.page.studentId, id]);
    }
  }

}
