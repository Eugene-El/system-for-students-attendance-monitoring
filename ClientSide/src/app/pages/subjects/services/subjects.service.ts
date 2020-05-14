import { Injectable } from '@angular/core';
import { DataService } from 'src/app/services/data-service/data.service';
import { SubjectGridModel } from '../models/subjectGridModel';
import { SubjectModel } from '../models/subjectModel';

@Injectable({
  providedIn: 'root'
})
export class SubjectsService {

  constructor(private dataService: DataService) { }

  private paths = {
    getAllForGrid: 'api/Subjects/GetAllForGrid/',
    get: 'api/Subjects/Get/',
    save: 'api/Subjects/Save/'
  }

  public getAllForGrid() : Promise<Array<SubjectGridModel>> {
    return this.dataService.get(this.paths.getAllForGrid);
  }

  public get(id: number) : Promise<SubjectModel> {
    return this.dataService.get(this.paths.get + id);
  }

  public save(faculty: SubjectModel): Promise<void> {
    return this.dataService.post(this.paths.save, faculty);
  }

}
