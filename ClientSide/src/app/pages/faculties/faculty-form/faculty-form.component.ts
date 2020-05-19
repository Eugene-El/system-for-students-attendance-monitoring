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
import { Sort } from '@angular/material/sort';
import { TranslateService } from '@ngx-translate/core';

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
    private translateService: TranslateService,
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
    this.methods.getCurrentLanguage();
    this.translateService.onLangChange.subscribe(() => { this.methods.getCurrentLanguage(); });
  }

  page = {
    facultyId: 0 as number,
    faculty: null as FacultyModel,
    currentLanguage: null as string
  }

  dataSources = {
    studyProgrammes: new Array<StudyProgrammeModel>(),
    sortedStudyProgrammes: new MatTableDataSource<StudyProgrammeModel>()
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
      this.dataSources.studyProgrammes = studyProgrammes;
      this.dataSources.sortedStudyProgrammes = new MatTableDataSource(studyProgrammes);
      this.dataSources.sortedStudyProgrammes.paginator = this.paginator;
    },
    addNewStudyProgramme: () => {
      let faculties = this.dataSources.studyProgrammes;
      let minIndex = 0;
      faculties.forEach(f => {
        minIndex = minIndex < f.id ? minIndex : f.id;
      });
      this.methods.openPopup(new StudyProgrammeModel(minIndex - 1, "", "", "", "", 0));
    },
    getCurrentLanguage: () => {
      this.page.currentLanguage = this.translateService.currentLang;
    },
    translateTitle: (sp: StudyProgrammeModel) => {
      switch(this.page.currentLanguage)
      {
        case "lv-LV":
          return sp.titleLv;
        case "ru-RU":
          return sp.titleRu;
        case "en-GB":
        default:
          return sp.titleEn;
      }
    },
    applyStudyProgrammeFilter: (e) => {
      let filterValue = (e.target as HTMLInputElement).value;
      this.dataSources.sortedStudyProgrammes.filter = filterValue.trim().toLowerCase();

      if (this.dataSources.sortedStudyProgrammes.paginator)
        this.dataSources.sortedStudyProgrammes.paginator.firstPage();
    },
    sortData: (sort: Sort) => {

      let data = this.dataSources.studyProgrammes.slice(); // create new array insatnce
      if (sort.direction === '') {
        let filter = this.dataSources.sortedStudyProgrammes.filter;
        this.dataSources.sortedStudyProgrammes = new MatTableDataSource(data);
        this.dataSources.sortedStudyProgrammes.paginator = this.paginator;
        this.dataSources.sortedStudyProgrammes.filter = filter;
        return;
      }
      
      let sortedData = data.sort((a, b) => {
        let isAsc = sort.direction === 'asc';
        switch (sort.active) {
          case 'code': return this.methods.compare(a.code, b.code, isAsc);
          case 'title': return this.methods.compare(this.methods.translateTitle(a), this.methods.translateTitle(b), isAsc);
          default: return 0;
        }
      });

      let filter = this.dataSources.sortedStudyProgrammes.filter;
      this.dataSources.sortedStudyProgrammes = new MatTableDataSource(sortedData);
      this.dataSources.sortedStudyProgrammes.paginator = this.paginator;
      this.dataSources.sortedStudyProgrammes.filter = filter;

    },
    compare: (a: number | string, b: number | string, isAsc: boolean) => {
      return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
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
      let faculties = this.dataSources.studyProgrammes;
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
        
        let faculties = this.dataSources.studyProgrammes;
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

      this.page.faculty.studyProgrammes = this.dataSources.studyProgrammes;
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
