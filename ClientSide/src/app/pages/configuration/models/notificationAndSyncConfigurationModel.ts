export class NotificationAndSyncConfigurationModel {

    public dataSyncPeriodHours: number;
    public notificationsPeriodHours: number; 

    constructor(
        dataSyncPeriodHours: number,
        notificationsPeriodHours: number
    ) {
        this.dataSyncPeriodHours = dataSyncPeriodHours;
        this.notificationsPeriodHours = notificationsPeriodHours;
    }

}
