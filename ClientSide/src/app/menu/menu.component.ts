import { Component, OnInit } from '@angular/core';
import { MenuItem } from './models/menuItem';
import { TranslateService } from '@ngx-translate/core';
import { LanguageItem } from './models/languageItem';
import { AuthorizationService } from '../services/authorization-service/authorization.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  constructor(
    private translateService: TranslateService,
    private authorizationService: AuthorizationService
  ) { }

  ngOnInit() {
    this.methods.fillMenu();
    this.methods.fillLanguages();
    this.methods.getCurrentLanguage();
  }

  page = {
    currentLanguage: ""
  }

  dataSources = {
    menuItems: new Array<MenuItem>(),
    languageItems: new Array<LanguageItem>()
  }

  methods = {
    fillMenu: () => {
      this.dataSources.menuItems = new Array<MenuItem>();

      this.dataSources.menuItems.push(new MenuItem("HEADERS.HOME", "/home"));
      if (this.authorizationService.isWorker())
        this.dataSources.menuItems.push(new MenuItem("HEADERS.FACULTIES", "/faculties"));
      if (this.authorizationService.isWorker())
        this.dataSources.menuItems.push(new MenuItem("HEADERS.SUBJECTS", "/subjects"));
      if (this.authorizationService.isWorkerOrLecturer())
        this.dataSources.menuItems.push(new MenuItem("HEADERS.STUDENTS", "/students"));
      if (this.authorizationService.isWorker())
        this.dataSources.menuItems.push(new MenuItem("HEADERS.CONFIGURATION", "/configuration"));
      if (this.authorizationService.isStudent())
        this.dataSources.menuItems.push(new MenuItem("HEADERS.SEE_OWN_ATTENDANCE", "/seeOwnAttendance"));
      if (this.authorizationService.isWorker())
        this.dataSources.menuItems.push(new MenuItem("HEADERS.NOTIFICATIONS", "/notifications"));
      if (this.authorizationService.isWorker())
        this.dataSources.menuItems.push(new MenuItem("HEADERS.NOTIFICATION_HISTORY", "/notificationHistories"));
    },
    fillLanguages: () => {
      this.dataSources.languageItems = new Array<LanguageItem>();

      this.dataSources.languageItems.push(new LanguageItem("en-GB", "EN"));
      this.dataSources.languageItems.push(new LanguageItem("lv-LV", "LV"));
      this.dataSources.languageItems.push(new LanguageItem("ru-RU", "RU"));
    },
    getCurrentLanguage: () => {
      this.page.currentLanguage = this.translateService.currentLang;
    },
    changeLanguage: (lang: string) => {
      this.translateService.use(lang).subscribe(() => {
        localStorage.setItem("Language", lang);
        this.methods.getCurrentLanguage();
      });
    },
    logout: () => {
      this.authorizationService.logout();
    }
  }
}
