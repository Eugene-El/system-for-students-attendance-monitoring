import { NotificationAndSyncConfigurationModel } from './notificationAndSyncConfigurationModel';

export class ConfigurationModel {

    public notificationAndSyncConfiguration: NotificationAndSyncConfigurationModel;

    constructor(
        notificationAndSyncConfiguration: NotificationAndSyncConfigurationModel
    ) {
        this.notificationAndSyncConfiguration = notificationAndSyncConfiguration;
    }
}
