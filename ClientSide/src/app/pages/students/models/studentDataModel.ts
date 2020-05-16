import { StudentModel } from './studentModel';
import { ExtraSelectModel } from 'src/app/common/models/extraSelectModel';

export class StudentDataModel {

    public student: StudentModel;
    public faculties: Array<ExtraSelectModel>;

    constructor (
        student: StudentModel,
        faculties: Array<ExtraSelectModel>
    ) {
        this.student = student;
        this.faculties = faculties;
    }

}
