import { Component, OnInit } from '@angular/core';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { StudentAttendancesService } from '../services/student-attendances.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { StudentAttendanceModel } from '../models/studentAttendanceModel';
import { SelectModel } from 'src/app/common/models/selectModel';
import { Moment } from 'moment';

@Component({
  selector: 'app-student-attendance-form',
  templateUrl: './student-attendance-form.component.html',
  styleUrls: ['./student-attendance-form.component.css']
})
export class StudentAttendanceFormComponent implements OnInit {

  constructor(
    private loadingService: LoadingService,
    private studentAttendancesService: StudentAttendancesService,
    private activatedRoute: ActivatedRoute,
    private notificationService: NotificationService,
    private router: Router
  ) { }

  ngOnInit() {
    this.loadingService.endLoading();
    this.page.studentId = +this.activatedRoute.snapshot.paramMap.get("studentId");
    this.page.studentAttendanceId = +this.activatedRoute.snapshot.paramMap.get("id");
    this.methods.getStudentAttendance();
  }

  page = {
    studentId: 0 as number,
    studentAttendanceId: 0 as number,
    studentAttendance: null as StudentAttendanceModel
  }

  dataSources = {
    subjects: new Array<SelectModel>(),
  }

  methods = {
    getStudentAttendance: () => {
      this.loadingService.addLoading();
      this.studentAttendancesService.get(this.page.studentAttendanceId)
        .then((studentAttendanceEditData) => {
          this.page.studentAttendance = studentAttendanceEditData.studentAttendance;
          this.dataSources.subjects = studentAttendanceEditData.subjects;

          if (this.page.studentAttendance == null)
            this.page.studentAttendance = new StudentAttendanceModel(0, this.page.studentId, null, new Date(), 0, 0);

          this.loadingService.endLoading();
        })
        .catch((error) => {
          this.notificationService.processError(error);
          this.loadingService.endLoading();
        });
    },
    save: () => {
      this.loadingService.addLoading();
      if ((this.page.studentAttendance.date as any).toDate) // fix date
      {
        this.page.studentAttendance.date = (this.page.studentAttendance.date as any).toDate();
        this.page.studentAttendance.date = new Date(Date.UTC(
          this.page.studentAttendance.date.getFullYear(),
          this.page.studentAttendance.date.getMonth(),
          this.page.studentAttendance.date.getDate()) as any);
      }
      this.studentAttendancesService.save(this.page.studentAttendance)
        .then(() => {
          this.loadingService.endLoading();
          this.notificationService.success("COMMON.SUCESSFULLY_SAVED");
          this.methods.back();
        })
        .catch((error) => {
          this.notificationService.processError(error);
          this.loadingService.endLoading();
        })
    },
    back: () => {
      this.router.navigate(['students', 'attendances', this.page.studentId]);
    }
  }

}
