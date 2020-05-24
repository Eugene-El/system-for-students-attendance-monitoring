import { SelectModel } from 'src/app/common/models/selectModel';

export class NotificationRuleModel {

    public id: number;
    public name: string;
    public notificationMethod: number;
    public studyProgrammeId: number;
    public language: number;
    public learningForm: number;
    public attendancePeriod: number;
    public attendanceProcent: number;
    public message: string;

    public facultyId: number; // Only client field
    public editing: boolean; // Only client field
    public studyProgrammes: Array<SelectModel>; // Only client field (for individual datasource)

    constructor(
        id: number,
        name: string,
        notificationMethod: number,
        studyProgrammeId: number,
        language: number,
        learningForm: number,
        attendancePeriod: number,
        attendanceProcent: number,
        message: string,
        facultyId: number,
        editing: boolean
    ) {
        this.id = id;
        this.name = name;
        this.notificationMethod = notificationMethod;
        this.studyProgrammeId = studyProgrammeId;
        this.language = language;
        this.learningForm = learningForm;
        this.attendancePeriod = attendancePeriod;
        this.attendanceProcent = attendanceProcent;
        this.message = message;

        this.facultyId = facultyId;
        this.editing = editing;
    }


}
