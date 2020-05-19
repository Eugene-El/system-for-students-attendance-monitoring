import { Component, OnInit, ViewChild } from '@angular/core';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { FacultyModel } from '../models/facultyModel';
import { FacultiesService } from '../services/faculties.service';
import { ActivatedRoute, Router } from '@angular/router';
import { StudyProgrammeModel } from '../models/studyProgrammeModel';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { StudyProgrammePopupComponent } from './study-programme-popup/study-programme-popup.component';
import { NotificationService } from 'src/app/services/notification-service/notification.service';

@Component({
  selector: 'app-faculty-form',
  templateUrl: './faculty-form.component.html',
  styleUrls: ['./faculty-form.component.css']
})
export class FacultyFormComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor(
    private loadingService: LoadingService,
    private facultiesService: FacultiesService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private matDialog: MatDialog,
    private notificationService: NotificationService
  ) {

  }

  ngOnInit() {
    this.loadingService.endLoading();
    this.page.facultyId = +this.activatedRoute.snapshot.paramMap.get("id");
    this.methods.getFaculty();
  }

  page = {
    facultyId: 0 as number,
    faculty: null as FacultyModel
  }

  dataSources = {
    studyProgrammes: new MatTableDataSource<StudyProgrammeModel>()
  }

  methods = {
    getFaculty: () => {
      if (this.page.facultyId == 0) {
        this.page.faculty = new FacultyModel(0, "", "", "", "", "", "", "", []);
        this.methods.setStudyProgrammesDataSource(this.page.faculty.studyProgrammes);
      }
      else
      {
        this.loadingService.addLoading();
        this.facultiesService.get(this.page.facultyId)
          .then((faculty) => {
            this.page.faculty = faculty;
            this.methods.setStudyProgrammesDataSource(this.page.faculty.studyProgrammes);
            this.loadingService.endLoading();
          })
          .catch((error) => {
            this.notificationService.processError(error);
            this.loadingService.endLoading();
          });
      }
    },
    setStudyProgrammesDataSource: (studyProgrammes: Array<StudyProgrammeModel>) => {
      this.dataSources.studyProgrammes = new MatTableDataSource(studyProgrammes);
      this.dataSources.studyProgrammes.paginator = this.paginator;
    },
    addNewStudyProgramme: () => {
      let faculties = this.dataSources.studyProgrammes.connect().getValue();
      let minIndex = 0;
      faculties.forEach(f => {
        minIndex = minIndex < f.id ? minIndex : f.id;
      });
      this.methods.openPopup(new StudyProgrammeModel(minIndex - 1, "", "", "", "", 0));
    },
    applyStudyProgrammeFilter: (e) => {
      let filterValue = (e.target as HTMLInputElement).value;
      this.dataSources.studyProgrammes.filter = filterValue.trim().toLowerCase();

      if (this.dataSources.studyProgrammes.paginator)
        this.dataSources.studyProgrammes.paginator.firstPage();
    },
    editStudyProgramme: (studyProgramme: StudyProgrammeModel) => {
      this.methods.openPopup(new StudyProgrammeModel(
        studyProgramme.id,
        studyProgramme.code,
        studyProgramme.titleEn,
        studyProgramme.titleLv,
        studyProgramme.titleRu,
        studyProgramme.studentsCount
      ));
    },
    deleteStudyProgramme: (id: number) => {
      let faculties = this.dataSources.studyProgrammes.connect().getValue();
      faculties = faculties.filter(f => f.id != id);
      this.methods.setStudyProgrammesDataSource(faculties);
    },
    openPopup: (studyProgramme: StudyProgrammeModel) => {
      let dialogRef = this.matDialog.open(StudyProgrammePopupComponent, {
        width: '50%',
        data: studyProgramme
      });
  
      dialogRef.afterClosed().subscribe((result: StudyProgrammeModel) => {

        if (result == undefined)
          return;
        
        let faculties = this.dataSources.studyProgrammes.connect().getValue();
        let faculty = faculties.find(f => f.id == result.id);
        if (faculty) {
          faculty.code = result.code;
          faculty.titleEn = result.titleEn;
          faculty.titleLv = result.titleLv;
          faculty.titleRu = result.titleRu;
        } else {
          faculties.push(result);
        }
        this.methods.setStudyProgrammesDataSource(faculties);
      });
    },
    save: () => {
      if (this.page.faculty.code == "" || this.page.faculty.titleEn == "" || this.page.faculty.titleLv == "" ||
        this.page.faculty.titleRu == "" || this.page.faculty.shortTitleEn == "" || this.page.faculty.shortTitleLv == "" ||
        this.page.faculty.shortTitleRu == "") {
        this.notificationService.fillAllFields();
        return;
      }

      this.page.faculty.studyProgrammes = this.dataSources.studyProgrammes.connect().getValue();
      this.page.faculty.studyProgrammes.forEach(s => { s.id = s.id <= 0 ? 0 : s.id; });
      this.loadingService.addLoading();
      this.facultiesService.save(this.page.faculty)
        .then(() => {
          this.loadingService.endLoading();
          this.notificationService.successfullySaved();
          this.methods.back();
        }, (error) => {
          this.notificationService.processError(error);
          this.loadingService.endLoading();
        })
    },
    back: () => {
      this.router.navigate(['faculties']);
    }
  }
}
