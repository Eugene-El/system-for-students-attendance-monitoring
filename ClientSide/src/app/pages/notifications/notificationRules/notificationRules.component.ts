import { Component, OnInit } from '@angular/core';
import { ExtraSelectModel } from 'src/app/common/models/extraSelectModel';
import { SelectModel } from 'src/app/common/models/selectModel';
import { StudentsService } from '../../students/services/students.service';
import { NotificationRuleModel } from '../models/notificationRuleModel';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { NotificationDataService } from '../services/notificationData.service';
import { TranslateService } from '@ngx-translate/core';
import { MatSelectChange } from '@angular/material/select';
import { MatDialog } from '@angular/material/dialog';
import { AreYouSurePopupComponent } from 'src/app/common/components/areYouSurePopup/areYouSurePopup.component';

@Component({
  selector: 'app-notificationRules',
  templateUrl: './notificationRules.component.html',
  styleUrls: ['./notificationRules.component.css']
})
export class NotificationRulesComponent implements OnInit {

  constructor(
    private studentsService: StudentsService,
    private loadingService: LoadingService,
    private notificationService: NotificationService,
    private notificationDataService: NotificationDataService,
    private translateService: TranslateService,
    private matDialog: MatDialog
  ) { }

  ngOnInit() {
    this.loadingService.endLoading();
    this.methods.getNotificationRules();
    this.translateService.onLangChange.subscribe(() => {
      this.methods.getNotificationRules();
    })
  }

  page = {
    isEditing: false as boolean
  }

  dataSources = {
    notificationRules: new Array<NotificationRuleModel>(),

    faculties: new Array<ExtraSelectModel>(),
    
    studentLanguages: this.studentsService.getStudentLanguages(),
    studentLearningForms: this.studentsService.getLearningForms(),
    notificationMethods: this.notificationDataService.getNotificationMethods()
  }

  methods = {
    getNotificationRules: () => {
      this.loadingService.addLoading();
      this.notificationDataService.getAll()
        .then((notificationData) => {
          this.dataSources.faculties = notificationData.faculties;
          this.dataSources.notificationRules = notificationData.notificationRules;
          this.dataSources.notificationRules.forEach((notificationRule) => {
            let faculty = this.methods.findFactulty(notificationRule.studyProgrammeId); //this.dataSources.faculties.find(f => f.options.some(s => s.id == notificationRule.id));
            if (faculty != null) {
              notificationRule.facultyId = faculty.id;
              notificationRule.studyProgrammes = faculty.options;
            }
          })
          this.page.isEditing = false;
          this.loadingService.endLoading();
        })
        .catch((error)=> {
          this.notificationService.processError(error);
          this.loadingService.endLoading();
        });
    },
    findFactulty: (studyProgrammeId: number) => {
      for (let f = 0; f < this.dataSources.faculties.length; f++)
        for (let s = 0; s < this.dataSources.faculties[f].options.length; s++)
          if (this.dataSources.faculties[f].options[s].id == studyProgrammeId)
            return this.dataSources.faculties[f];
      return null;
    },
    addNew: () => {
      let notificationRules = this.dataSources.notificationRules.reverse();
      notificationRules.push(new NotificationRuleModel(0, "", null, null, null, null, 30, 20, "", null, true));
      this.page.isEditing = true;
      this.dataSources.notificationRules = notificationRules.reverse();
    },
    edit: (notificationRule: NotificationRuleModel) => {
      this.page.isEditing = true;
      notificationRule.editing = true;
    },
    save: (notificationRule: NotificationRuleModel) => {

      if (notificationRule.name == "" || notificationRule.notificationMethod == null || notificationRule.studyProgrammeId == null ||
        notificationRule.language == null || notificationRule.learningForm == null || notificationRule.attendanceProcent == null ||
        notificationRule.message == "") {
          this.notificationService.fillAllFields();
          return;
      }

      this.loadingService.addLoading();
      this.notificationDataService.save(notificationRule)
        .then(() => {
          this.loadingService.endLoading();
          this.methods.getNotificationRules();
        })
        .catch((error)=> {
          this.notificationService.processError(error);
          this.loadingService.endLoading();
        });
    },
    delete: (notificationRule: NotificationRuleModel) => {

      console.log(notificationRule);

      const dialogRef = this.matDialog.open(AreYouSurePopupComponent, {
        width: 'auto',
      });
  
      dialogRef.afterClosed().subscribe(result => {
        
        if (result)
        {
          this.loadingService.addLoading();
      console.log(notificationRule);

          this.notificationDataService.delete(notificationRule.id)
            .then(() => {
              this.loadingService.endLoading();
              this.methods.getNotificationRules();
            })
            .catch((error => {
              this.notificationService.processError(error);
              this.loadingService.endLoading();
            }));
          }
      });
    },
    cancel: (notificationRule: NotificationRuleModel) => {
      this.page.isEditing = false;
      this.dataSources.notificationRules = this.dataSources.notificationRules.filter(n => n.id != notificationRule.id);
    },
    onFacultyChange: (e: MatSelectChange, notificationRule: NotificationRuleModel) => {
      if (e.value == undefined)
        return;
      
      let faculty = this.dataSources.faculties.find(f => f.id == e.value);
      if (faculty != null)
        notificationRule.studyProgrammes = faculty.options;
    },
  }
}
