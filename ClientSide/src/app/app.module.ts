import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateLoader, TranslateModule, TranslateService, MissingTranslationHandler, MissingTranslationHandlerParams } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { AppComponent } from './app.component';
import { MenuComponent } from './menu/menu.component';
import { AppRoutingModule } from './routing/app-routing.module';
import { HomeComponent } from './pages/home/home.component';
import { FacultiesListComponent } from './pages/faculties/faculties-list/faculties-list.component';
import { FacultyFormComponent } from './pages/faculties/faculty-form/faculty-form.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MomentDateModule, MomentDateAdapter } from '@angular/material-moment-adapter';

import { StudyProgrammePopupComponent } from './pages/faculties/faculty-form/study-programme-popup/study-programme-popup.component';
import { SubjectsListComponent } from './pages/subjects/subjects-list/subjects-list.component';
import { SubjectFormComponent } from './pages/subjects/subject-form/subject-form.component';
import { StudentsListComponent } from './pages/students/students-list/students-list.component';
import { StudentFormComponent } from './pages/students/student-form/student-form.component';
import { NotificationComponent } from './common/components/notification/notification.component';
import { StudentAttendancesListComponent } from './pages/studentAttendances/student-attendances-list/student-attendances-list.component';
import { StudentAttendanceFormComponent } from './pages/studentAttendances/student-attendance-form/student-attendance-form.component';
import { ConfigurationComponent } from './pages/configuration/configuration.component';
import { AuthorizationComponent } from './pages/authorization/authorization.component';

// Handle no translation case
export class CustomMissingTranslationHandler implements MissingTranslationHandler {
   handle(params: MissingTranslationHandlerParams) {
      const keyParts = params.key.split(".");
      return keyParts.pop();
   }
}
export const MY_FORMATS = {
   parse: {
      dateInput: 'LL',
   },
   display: {
      dateInput: 'DD.MM.YYYY',
      monthYearLabel: 'MMM YYYY',
      dateA11yLabel: 'LL',
      monthYearA11yLabel: 'MMMM YYYY',
   },
 };

@NgModule({
   declarations: [
      AppComponent,
      MenuComponent,
      AuthorizationComponent,
      NotificationComponent,
      HomeComponent,
      FacultiesListComponent,
      FacultyFormComponent,
      StudyProgrammePopupComponent,
      SubjectsListComponent,
      SubjectFormComponent,
      StudentsListComponent,
      StudentFormComponent,
      StudentAttendancesListComponent,
      StudentAttendanceFormComponent,
      ConfigurationComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      TranslateModule.forRoot({
         loader: {
         provide: TranslateLoader,
         useFactory: httpTranslateLoader,
         deps: [HttpClient]
         },
         //missingTranslationHandler: {provide: MissingTranslationHandler, useClass: CustomMissingTranslationHandler},
      }),
      BrowserAnimationsModule,      
      FormsModule,
      ReactiveFormsModule,
      MatButtonModule,
      MatIconModule,
      MatTableModule,
      MatPaginatorModule,
      MatSortModule,
      MatProgressSpinnerModule,
      MatFormFieldModule,
      MatInputModule,
      MatDialogModule,
      MatSelectModule,
      MatDatepickerModule,
      MatNativeDateModule
      
   ],
   providers: [
      {
         provide: APP_INITIALIZER,
         useFactory: appInitializerFactory,
         deps: [TranslateService],
         multi: true
      },
      {
         provide: DateAdapter,
         useClass: MomentDateAdapter,
         deps: [MAT_DATE_LOCALE]
      },
      {
         provide: MAT_DATE_FORMATS,
         useValue: MY_FORMATS
      },
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }

// Find all localizations
export function httpTranslateLoader(http: HttpClient) {
   return new TranslateHttpLoader(http, "./assets/i18n/", ".json");
}

// Wait for localizations load
export function appInitializerFactory(translateService: TranslateService) {
   return () => new Promise<any>((resolve: any) => {
      translateService.addLangs(["en-GB", "ru-RU", "lv-LV"]);
      let language = localStorage.getItem("Language");
      if (!translateService.langs.includes(language))
      {
         language = "en-GB";
         localStorage.setItem("Language", language);
      }
      translateService.use(language).subscribe(() => {}, () => {},
         () => { resolve(null); });
   });
 }