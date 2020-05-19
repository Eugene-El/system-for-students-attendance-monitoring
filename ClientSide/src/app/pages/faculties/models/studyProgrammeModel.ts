export class StudyProgrammeModel {

    public id: number;
    public code: string;
    
    public titleEn: string;
    public titleLv: string;
    public titleRu: string;

    public studentsCount: number;

    constructor(
        id: number,
        code: string,
        titleEn: string,
        titleLv: string,
        titleRu: string,
        studentsCount: number
    ) {
        this.id = id;
        this.code = code;
        this.titleEn = titleEn;
        this.titleLv = titleLv;
        this.titleRu = titleRu;
        this.studentsCount = studentsCount;
    }
}
