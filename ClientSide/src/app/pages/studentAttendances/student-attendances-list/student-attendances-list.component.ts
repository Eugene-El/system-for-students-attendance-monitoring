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
    studentAttendances: new MatTableDataSource<StudentAttendanceGridModel>(),
  }

  methods = {
    getStudentAttendances: () => {
      this.loadingService.addLoading();
      this.studentAttendancesService.getAllForGrid(this.page.studentId).then((studentData) => {
        this.dataSources.studentAttendances = new MatTableDataSource(studentData.studentAttandances);
        this.page.student = studentData.student;
        this.dataSources.studentAttendances.paginator = this.paginator;
        this.loadingService.endLoading();
      }).catch((error) => {
        this.notificationService.processError(error);
        this.loadingService.endLoading();
      });
    },
    applyFilter: (e) => {
      let filterValue = (e.target as HTMLInputElement).value;
      this.dataSources.studentAttendances.filter = filterValue.trim().toLowerCase();

      if (this.dataSources.studentAttendances.paginator)
        this.dataSources.studentAttendances.paginator.firstPage();
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
