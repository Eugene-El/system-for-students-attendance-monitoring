import { Injectable } from '@angular/core';
import { DataService } from 'src/app/services/data-service/data.service';
import { StudentGridModel } from '../models/studentGridModel';
import { StudentModel } from '../models/studentModel';
import { StudentDataModel } from '../models/studentDatamodel';
import { SelectModel } from 'src/app/common/models/selectModel';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {

  constructor(private dataService: DataService) { }

  private paths = {
    getAllForGrid: 'api/Students/GetAllForGrid/',
    get: 'api/Students/Get/',
    save: 'api/Students/Save/'
  }

  public getAllForGrid() : Promise<Array<StudentGridModel>> {
    return this.dataService.get(this.paths.getAllForGrid);
  }

  public get(id: number) : Promise<StudentDataModel> {
    return this.dataService.get(this.paths.get + id);
  }

  public save(faculty: StudentModel): Promise<void> {
    return this.dataService.post(this.paths.save, faculty);
  }

  // Enumarations
  public getLearningForms() {
    return [
      new SelectModel(1, "STUDENT.LEARNING_FORMS.DAYTIME"),
      new SelectModel(2, "STUDENT.LEARNING_FORMS.EVENINGS"),
      new SelectModel(3, "STUDENT.LEARNING_FORMS.WEEKENDS"),
      new SelectModel(4, "STUDENT.LEARNING_FORMS.DISTANCE")
    ];
  }
  public getLearningFormsDictionary() {
    return this.makeDictionary(this.getLearningForms());
  }
  public getStudentLanguages() {
    return [
      new SelectModel(1, "STUDENT.LANGUAGES.ENGLISH"),
      new SelectModel(2, "STUDENT.LANGUAGES.LATVIAN"),
      new SelectModel(3, "STUDENT.LANGUAGES.RUSSIAN"),
    ];
  }
  public getStudentLanguagesDictionary() {
    return this.makeDictionary(this.getStudentLanguages());
  }
  public getStudentStatuses() {
    return [
      new SelectModel(1, "STUDENT.STATUSES.ACTIVE"),
      new SelectModel(2, "STUDENT.STATUSES.INACTIVE"),
    ];
  }

  private makeDictionary(values: Array<SelectModel>) {
    let dictionary = {};
    values.forEach(val => dictionary[val.id] = val.title);
    return dictionary;
  }


}
