import { StudentShortModel } from './studentShortModel';
import { StudentAttendanceGridModel } from './studentAttendanceGridModel';

export class StudentAttendanceDataModel {

    public student: StudentShortModel;
    public studentAttandances: Array<StudentAttendanceGridModel>

    constructor(
        student: StudentShortModel,
        studentAttandances: Array<StudentAttendanceGridModel>
    ) {
        this.student = student;
        this.studentAttandances = studentAttandances;
    }

}
