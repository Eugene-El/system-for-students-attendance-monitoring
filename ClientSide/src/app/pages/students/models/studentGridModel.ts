export class StudentGridModel {
    
    public id: number;
    public code: string;
    public fullName: string;
    public facultyTitle: string;
    public studyProgrammeTitle: string;
    public learningForm: string;
    public studentLanguage: string;

    constructor(
        id: number,
        code: string,
        fullName: string,
        facultyTitle: string,
        studyProgrammeTitle: string,
        learningForm: string,
        studentLanguage: string,
    ) {
        this.id = id;
        this.code = code;
        this.fullName = fullName;
        this.facultyTitle = facultyTitle;
        this.studyProgrammeTitle = studyProgrammeTitle;
        this.learningForm = learningForm;
        this.studentLanguage = studentLanguage;
    }
}
