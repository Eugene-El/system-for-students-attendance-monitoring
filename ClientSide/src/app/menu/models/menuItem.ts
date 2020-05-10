export class MenuItem {

    public localizationKey: string;
    public link: string;

    constructor(
        localizationKey: string,
        link: string
    ) {
        this.localizationKey = localizationKey;
        this.link = link;
    }

}
