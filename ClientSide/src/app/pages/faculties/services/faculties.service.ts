import { Injectable } from '@angular/core';
import { DataService } from 'src/app/services/data-service/data.service';
import { FacultyGridModel } from '../models/facultyGridModel';
import { FacultyModel } from '../models/facultyModel';

@Injectable({
  providedIn: 'root'
})
export class FacultiesService {

  constructor(private dataService: DataService) { }

  private paths = {
    getAllForGrid: 'api/Faculties/GetAllForGrid/',
    get: 'api/Faculties/Get/',
    save: 'api/Faculties/Save/'
  }

  public getAllForGrid() : Promise<Array<FacultyGridModel>> {
    return this.dataService.get(this.paths.getAllForGrid);
  }

  public get(id: number) : Promise<FacultyModel> {
    return this.dataService.get(this.paths.get + id);
  }

  public save(faculty: FacultyModel): Promise<void> {
    return this.dataService.post(this.paths.save, faculty);
  }
}
