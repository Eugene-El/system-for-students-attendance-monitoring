import { Injectable } from '@angular/core';
import { DataService } from 'src/app/services/data-service/data.service';
import { HomeDataModel } from '../models/homeDataModel';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private dataService: DataService) { }


  private paths = {
    getFacultyStatistics: 'api/Home/GetFacultyStatistics/',
  }

  public getFacultyStatistics() : Promise<HomeDataModel> {
    return this.dataService.get(this.paths.getFacultyStatistics);
  }

}
