export class SubjectGridModel {

    public id: number;
    public code: string;
    public title: string;

    constructor(
        id: number,
        code: string,
        title: string,
    ) {
        this.id = id;
        this.code = code;
        this.title = title;
    }
}
