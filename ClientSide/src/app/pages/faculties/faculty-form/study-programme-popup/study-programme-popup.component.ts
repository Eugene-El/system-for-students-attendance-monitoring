import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { StudyProgrammeModel } from '../../models/studyProgrammeModel';

@Component({
    selector: 'study-programme-popup',
    templateUrl: 'study-programme-popup.component.html',
    styleUrls: ['../faculty-form.component.css']
})
export class StudyProgrammePopupComponent {

    constructor(
        private dialogRef: MatDialogRef<StudyProgrammePopupComponent>,
        @Inject(MAT_DIALOG_DATA) public data: StudyProgrammeModel
    ) {}

    close(): void {
        this.dialogRef.close();
    }
    
}