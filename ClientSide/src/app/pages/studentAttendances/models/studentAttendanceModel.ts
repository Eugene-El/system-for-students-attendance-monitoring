export class StudentAttendanceModel {

    public id: number;
    public studentId: number;
    public subjectId: number;
    public date: Date;
    public necessaryAttendance: number;
    public realAttendance: number;

    constructor(
        id: number,
        studentId: number,
        subjectId: number,
        date: Date,
        necessaryAttendance: number,
        realAttendance: number,
    ) {
        this.id = id;
        this.studentId = studentId;
        this.subjectId = subjectId;
        this.date = date;
        this.necessaryAttendance = necessaryAttendance;
        this.realAttendance = realAttendance;
    }
}
