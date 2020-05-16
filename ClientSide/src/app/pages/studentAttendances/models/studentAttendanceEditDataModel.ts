import { StudentAttendanceModel } from './studentAttendanceModel';
import { SelectModel } from 'src/app/common/models/selectModel';

export class StudentAttendanceEditDataModel {

    public studentAttendance: StudentAttendanceModel;
    public subjects: Array<SelectModel>;

    constructor(
        studentAttendance: StudentAttendanceModel,
        subjects: Array<SelectModel>
    ) {
        this.studentAttendance = studentAttendance;
        this.subjects = subjects;
    }
}
