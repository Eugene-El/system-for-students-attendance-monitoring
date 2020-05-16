export class StudentShortModel {

    public id: number;
    public code: string;
    public fullName: string;
    public studyProgrammeTitle: string;

    constructor(
        id: number,
        code: string,
        fullName: string,
        studyProgrammeTitle: string
    ) {
        this.id = id;
        this.code = code;
        this.fullName = fullName;
        this.studyProgrammeTitle = studyProgrammeTitle;
    }
}
