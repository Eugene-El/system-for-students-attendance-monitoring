import { Component, OnInit } from '@angular/core';
import { FacultiesService } from '../services/faculties.service';
import { LoadingService } from 'src/app/services/loading-service/loading.service';

@Component({
  selector: 'app-faculties-list',
  templateUrl: './faculties-list.component.html',
  styleUrls: ['./faculties-list.component.css']
})
export class FacultiesListComponent implements OnInit {

  constructor(
    private facultiesService: FacultiesService,
    private loadingService: LoadingService
  ) { }

  ngOnInit() {
    this.loadingService.endLoading();
    this.methods.getFaculties();
  }
  
  page = {

  }

  dataSources = {
    faculties: [],
    displayedColumns: ['code', 'title', 'studyProgrammeCount', 'actions']
  }

  methods = {
    getFaculties: () => {
      this.loadingService.addLoading();
      this.facultiesService.getAllForGrid().then((faculties) => {
        this.dataSources.faculties = faculties;
        this.loadingService.endLoading();
      }).catch((error) => {

        this.loadingService.endLoading();
      });
    },
    addNew: () => {
      this.loadingService.addLoading();
    }
  }
}
