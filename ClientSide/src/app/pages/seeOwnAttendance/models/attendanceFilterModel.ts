export class AttendanceFilterModel {

    public from: Date;
    public to: Date;

    constructor (
        from: Date,
        to: Date
    ) {
        this.from = from;
        this.to = to;
    }
}
