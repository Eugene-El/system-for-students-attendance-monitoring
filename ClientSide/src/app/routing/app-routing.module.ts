import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from '../pages/home/home.component';
import { FacultiesListComponent } from '../pages/faculties/faculties-list/faculties-list.component';
import { FacultyFormComponent } from '../pages/faculties/faculty-form/faculty-form.component';
import { SubjectsListComponent } from '../pages/subjects/subjects-list/subjects-list.component';
import { SubjectFormComponent } from '../pages/subjects/subject-form/subject-form.component';
import { StudentsListComponent } from '../pages/students/students-list/students-list.component';
import { StudentFormComponent } from '../pages/students/student-form/student-form.component';
import { StudentAttendancesListComponent } from '../pages/studentAttendances/student-attendances-list/student-attendances-list.component';
import { StudentAttendanceFormComponent } from '../pages/studentAttendances/student-attendance-form/student-attendance-form.component';
import { ConfigurationComponent } from '../pages/configuration/configuration.component';
import { AuthorizationComponent } from '../pages/authorization/authorization.component';
import { SeeOwnAttendanceComponent } from '../pages/seeOwnAttendance/seeOwnAttendance.component';

const routes: Routes = [

    { path: 'login', component: AuthorizationComponent },
    { path: 'home', component: HomeComponent },
    { path: 'faculties', component: FacultiesListComponent },
    { path: 'faculties/:id', component: FacultyFormComponent },
    { path: 'subjects', component: SubjectsListComponent },
    { path: 'subjects/:id', component: SubjectFormComponent },
    { path: 'students', component: StudentsListComponent },
    { path: 'students/:id', component: StudentFormComponent },
    { path: 'students/attendances/:studentId', component: StudentAttendancesListComponent },
    { path: 'students/attendances/:studentId/:id', component: StudentAttendanceFormComponent },
    { path: 'configuration', component: ConfigurationComponent },
    { path: 'seeOwnAttendance', component: SeeOwnAttendanceComponent },
    { path: '**', redirectTo: '/login' }

];
  
@NgModule({
imports: [RouterModule.forRoot(routes)],
exports: [RouterModule]
})
export class AppRoutingModule { }
  