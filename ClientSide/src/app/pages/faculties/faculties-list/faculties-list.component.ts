import { Component, OnInit, ViewChild } from '@angular/core';
import { FacultiesService } from '../services/faculties.service';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { FacultyGridModel } from '../models/facultyGridModel';
import { MatPaginator } from '@angular/material/paginator';
import { TranslateService } from '@ngx-translate/core';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { Sort } from '@angular/material/sort';

@Component({
  selector: 'app-faculties-list',
  templateUrl: './faculties-list.component.html',
  styleUrls: ['./faculties-list.component.css']
})
export class FacultiesListComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  
  constructor(
    private facultiesService: FacultiesService,
    private loadingService: LoadingService,
    private translateService: TranslateService,
    private notificationService: NotificationService,
    private router: Router
  ) { }

  ngOnInit() {
    this.loadingService.endLoading();
    this.methods.getFaculties();
    this.translateService.onLangChange.subscribe(() => { this.methods.getFaculties(); });
  }
  
  page = {

  }

  dataSources = {
    faculties: new Array<FacultyGridModel>(),
    sortedFaculties: new MatTableDataSource<FacultyGridModel>(),
    displayedColumns: ['code', 'title', 'studyProgrammeCount', 'actions']
  }

  methods = {
    getFaculties: () => {
      this.loadingService.addLoading();
      this.facultiesService.getAllForGrid().then((faculties) => {
        this.dataSources.faculties = faculties;
        this.dataSources.sortedFaculties = new MatTableDataSource(faculties);
        this.dataSources.sortedFaculties.paginator = this.paginator;
        this.loadingService.endLoading();
      }).catch((error) => {
        this.notificationService.processError(error);
        this.loadingService.endLoading();
      });
    },
    applyFilter: (e) => {
      let filterValue = (e.target as HTMLInputElement).value;
      this.dataSources.sortedFaculties.filter = filterValue.trim().toLowerCase();

      if (this.dataSources.sortedFaculties.paginator)
        this.dataSources.sortedFaculties.paginator.firstPage();
    },
    sortData: (sort: Sort) => {

      let data = this.dataSources.faculties.slice(); // create new array insatnce
      if (sort.direction === '') {
        let filter = this.dataSources.sortedFaculties.filter;
        this.dataSources.sortedFaculties = new MatTableDataSource(data);
        this.dataSources.sortedFaculties.paginator = this.paginator;
        this.dataSources.sortedFaculties.filter = filter;
        return;
      }
  
      let sortedData = data.sort((a, b) => {
        let isAsc = sort.direction === 'asc';
        switch (sort.active) {
          case 'code': return this.methods.compare(a.code, b.code, isAsc);
          case 'title': return this.methods.compare(a.title, b.title, isAsc);
          case 'studyProgrammeCount': return this.methods.compare(a.studyProgrammeCount, b.studyProgrammeCount, isAsc);
          default: return 0;
        }
      });

      let filter = this.dataSources.sortedFaculties.filter;
      this.dataSources.sortedFaculties = new MatTableDataSource(sortedData);
      this.dataSources.sortedFaculties.paginator = this.paginator;
      this.dataSources.sortedFaculties.filter = filter;

    },
    compare: (a: number | string, b: number | string, isAsc: boolean) => {
      return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
    },
    addNew: () => {
      this.router.navigate(['faculties', 0]);
    },
    edit: (id) => {
      this.router.navigate(['faculties', id]);
    }
  }
}
