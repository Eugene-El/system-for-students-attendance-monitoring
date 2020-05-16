export class StudentModel {
    
    public id: number;
    public code: string;
    public name: string;
    public surname: string;

    public studyProgrammeId: number;
    public language: number;
    public learningForm: number;
    public status: number;

    public phone1: string;
    public phone2: string;
    public email1: string;
    public email2: string;
    public skype: string;
    public comment: string;


    constructor(
        id: number,
        code: string,
        name: string,
        surname: string,

        studyProgrammeId: number,
        language: number,
        learningForm: number,
        status: number,

        phone1: string,
        phone2: string,
        email1: string,
        email2: string,
        skype: string,
        comment: string
    ) {
        this.id = id;
        this.code = code;
        this.name = name;
        this.surname = surname;

        this.studyProgrammeId = studyProgrammeId;
        this.language = language;
        this.learningForm = learningForm;
        this.status = status;

        this.phone1 = phone1;
        this.phone2 = phone2;
        this.email1 = email1;
        this.email2 = email2;
        this.skype = skype;
        this.comment = comment;
    }
}
