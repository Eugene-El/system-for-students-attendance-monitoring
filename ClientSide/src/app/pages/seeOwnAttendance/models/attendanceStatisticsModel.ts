import { DayStatisticsModel } from './dayStatisticsModel';
import { SubjectStatisticsModel } from './subjectStatisticsModel';

export class AttendanceStatisticsModel {

    public globalAttendanceProcent: number;
    public globalStatistics: Array<DayStatisticsModel>;
    public subjectStatistics: Array<SubjectStatisticsModel>;

}
