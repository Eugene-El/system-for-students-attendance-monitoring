export class SubjectModel {
    
    public id: number;
    public code: string;

    public titleEn: string;
    public titleLv: string;
    public titleRu: string;
    
    public shortTitleEn: string;
    public shortTitleLv: string;
    public shortTitleRu: string;
    

    constructor(
        id: number,
        code: string,
        titleEn: string,
        titleLv: string,
        titleRu: string,
        shortTitleEn: string,
        shortTitleLv: string,
        shortTitleRu: string
    ) {
        this.id = id;
        this.code = code;
        this.titleEn = titleEn;
        this.titleLv = titleLv;
        this.titleRu = titleRu;
        this.shortTitleEn = shortTitleEn;
        this.shortTitleLv = shortTitleLv;
        this.shortTitleRu = shortTitleRu;
    }
}
