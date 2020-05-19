import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { StudyProgrammeModel } from '../../models/studyProgrammeModel';
import { NotificationService } from 'src/app/services/notification-service/notification.service';

@Component({
    selector: 'study-programme-popup',
    templateUrl: 'study-programme-popup.component.html',
    styleUrls: ['../faculty-form.component.css']
})
export class StudyProgrammePopupComponent {

    constructor(
        private dialogRef: MatDialogRef<StudyProgrammePopupComponent>,
        @Inject(MAT_DIALOG_DATA) public data: StudyProgrammeModel,
        private notificationService: NotificationService
    ) {}

    submit = () => {
        if (this.data.code == "" || this.data.titleEn == "" ||
            this.data.titleLv == "" || this.data.titleRu == "") {
            this.notificationService.fillAllFields();
            return;
        }

        this.dialogRef.close(this.data);
    }
    close = () => {
        this.dialogRef.close();
    }
    
}