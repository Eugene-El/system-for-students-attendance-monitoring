import { Component, OnInit, ViewChild } from '@angular/core';
import { FacultiesService } from '../services/faculties.service';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { FacultyGridModel } from '../models/facultyGridModel';
import { MatPaginator } from '@angular/material/paginator';
import { TranslateService } from '@ngx-translate/core';

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
    faculties: new MatTableDataSource<FacultyGridModel>(),
    displayedColumns: ['code', 'title', 'studyProgrammeCount', 'actions']
  }

  methods = {
    getFaculties: () => {
      this.loadingService.addLoading();
      this.facultiesService.getAllForGrid().then((faculties) => {
        this.dataSources.faculties = new MatTableDataSource(faculties);
        this.dataSources.faculties.paginator = this.paginator;
        this.loadingService.endLoading();
      }).catch((error) => {
        this.loadingService.endLoading();
      });
    },
    applyFilter: (e) => {
      let filterValue = (e.target as HTMLInputElement).value;
      this.dataSources.faculties.filter = filterValue.trim().toLowerCase();

      if (this.dataSources.faculties.paginator)
        this.dataSources.faculties.paginator.firstPage();
    },
    addNew: () => {
      this.router.navigate(['faculties', 0]);
    },
    edit: (id) => {
      this.router.navigate(['faculties', id]);
    }
  }
}
