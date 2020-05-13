export class FacultyGridModel {

    public id: number;
    public code: string;
    public title: string;
    public studyProgrammeCount: number;

    constructor(
        id: number,
        code: string,
        title: string,
        studyProgrammeCount: number
    ) {
        this.id = id;
        this.code = code;
        this.title = title;
        this.studyProgrammeCount = studyProgrammeCount;
    }

}
