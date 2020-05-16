import { Injectable } from '@angular/core';
import { DataService } from 'src/app/services/data-service/data.service';
import { StudentAttendanceDataModel } from '../models/studentAttendanceDataModel';
import { StudentAttendanceModel } from '../models/studentAttendanceModel';
import { StudentAttendanceEditDataModel } from '../models/studentAttendanceEditDataModel';

@Injectable({
  providedIn: 'root'
})
export class StudentAttendancesService {

  constructor(private dataService: DataService) { }

  private paths = {
    getAllForGrid: 'api/StudentAttendances/GetAllForGrid/',
    get: 'api/StudentAttendances/Get/',
    save: 'api/StudentAttendances/Save/'
  }

  public getAllForGrid(studentId: number) : Promise<StudentAttendanceDataModel> {
    return this.dataService.get(this.paths.getAllForGrid + studentId);
  }

  public get(id: number) : Promise<StudentAttendanceEditDataModel> {
    return this.dataService.get(this.paths.get + id);
  }

  public save(faculty: StudentAttendanceModel): Promise<void> {
    return this.dataService.post(this.paths.save, faculty);
  }


}
