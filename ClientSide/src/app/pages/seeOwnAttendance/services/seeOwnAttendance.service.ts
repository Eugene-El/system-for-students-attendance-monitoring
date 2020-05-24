import { Injectable } from '@angular/core';
import { DataService } from 'src/app/services/data-service/data.service';
import { AttendanceStatisticsModel } from '../models/attendanceStatisticsModel';
import { AttendanceFilterModel } from '../models/attendanceFilterModel';

@Injectable({
  providedIn: 'root'
})
export class SeeOwnAttendanceService {

  constructor(private dataService: DataService) { }

  private paths = {
    getAttendanceStatistics: 'api/OwnAttendance/GetAttendanceStatistics/',
  }

  public getAllForGrid(filter: AttendanceFilterModel) : Promise<AttendanceStatisticsModel> {
    return this.dataService.post(this.paths.getAttendanceStatistics, filter);
  }

}
