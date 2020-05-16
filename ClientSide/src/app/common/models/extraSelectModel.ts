import { SelectModel } from './selectModel';

export class ExtraSelectModel {

    public id: number;
    public title: string;
    public options: Array<SelectModel>;

    constructor (
        id: number,
        title: string,
        options: Array<SelectModel>
    ) {
        this.id = id;
        this.title = title;
        this.options = options;
    }
}
