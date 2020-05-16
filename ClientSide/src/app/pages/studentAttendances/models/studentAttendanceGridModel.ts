export class StudentAttendanceGridModel {

    public id: number;
    public subjectTitle: string;
    public date: string;
    public necessaryAttendance: number;
    public realAttendance: number;

    constructor(
        id: number,
        subjectTitle: string,
        date: string,
        necessaryAttendance: number,
        realAttendance: number
    ) {
        this.id = id;
        this.subjectTitle = subjectTitle;
        this.date = date;
        this.necessaryAttendance = necessaryAttendance;
        this.realAttendance = realAttendance;
    }
}
