import { Component, OnInit } from '@angular/core';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { StudentsService } from '../services/students.service';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentModel } from '../models/studentModel';
import { ExtraSelectModel } from 'src/app/common/models/extraSelectModel';
import { SelectModel } from 'src/app/common/models/selectModel';
import { MatSelectChange } from '@angular/material/select';
import { NotificationService } from 'src/app/services/notification-service/notification.service';

@Component({
  selector: 'app-student-form',
  templateUrl: './student-form.component.html',
  styleUrls: ['./student-form.component.css']
})
export class StudentFormComponent implements OnInit {

  constructor(
    private loadingService: LoadingService,
    private studentsService: StudentsService,
    private activatedRoute: ActivatedRoute,
    private notificationService: NotificationService,
    private router: Router
  ) { }

  ngOnInit() {
    this.loadingService.endLoading();
    this.page.studentId = +this.activatedRoute.snapshot.paramMap.get("id");
    this.methods.getStudent();
  }

  page = {
    studentId: 0 as number,
    facultyId: null as number,
    student: null as StudentModel
  }

  dataSources = {
    faculties: new Array<ExtraSelectModel>(),
    studyProgrammes: new Array<SelectModel>(),

    studentLanguages: this.studentsService.getStudentLanguages(),
    studentLearningForms: this.studentsService.getLearningForms(),
    studentStatuses: this.studentsService.getStudentStatuses(),
  }

  methods = {
    getStudent: () => {
      this.loadingService.addLoading();
      this.studentsService.get(this.page.studentId)
        .then((studentData) => {
          this.page.student = studentData.student;
          this.dataSources.faculties = studentData.faculties;

          if (this.page.student == null)
            this.page.student = new StudentModel(0, "", "", "", null, null, null, null, "", "", "", "", "", "");
          else
          {
            let faculty = this.dataSources.faculties.find(f => f.options.some(s => s.id == this.page.student.studyProgrammeId));
            if (faculty) {
              this.page.facultyId = faculty.id;
              this.dataSources.studyProgrammes = faculty.options;
            }
          }

          this.loadingService.endLoading();
        })
        .catch((error) => {
          this.notificationService.processError(error);
          this.loadingService.endLoading();
        });
    },
    onFacultyChange: (e: MatSelectChange) => {
      if (e.value == undefined)
        return;
      
      let faculty = this.dataSources.faculties.find(f => f.id == e.value);
      if (faculty != null)
        this.dataSources.studyProgrammes = faculty.options;
    },
    save: () => {
      if (this.page.student.code == "" || this.page.student.name == "" || this.page.student.surname == "" ||
        this.page.student.studyProgrammeId == null || this.page.student.language == null ||
        this.page.student.learningForm == null || this.page.student.status == null ||
        this.page.student.phone1 == "" || this.page.student.email1 == "") {
        this.notificationService.fillAllFields();
        return;
      }

      this.loadingService.addLoading();
      this.studentsService.save(this.page.student)
        .then(() => {
          this.loadingService.endLoading();
          this.notificationService.successfullySaved();
          this.methods.back();
        })
        .catch((error) => {
          this.notificationService.processError(error);
          this.loadingService.endLoading();
        })
    },
    back: () => {
      this.router.navigate(['students']);
    }
  }
}
