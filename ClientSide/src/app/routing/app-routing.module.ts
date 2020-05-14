import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from '../pages/home/home.component';
import { FacultiesListComponent } from '../pages/faculties/faculties-list/faculties-list.component';
import { FacultyFormComponent } from '../pages/faculties/faculty-form/faculty-form.component';

const routes: Routes = [

    { path: 'home', component: HomeComponent },
    { path: 'faculties', component: FacultiesListComponent },
    { path: 'faculties/:id', component: FacultyFormComponent },
    { path: '**', redirectTo: '/home' }

];
  
@NgModule({
imports: [RouterModule.forRoot(routes)],
exports: [RouterModule]
})
export class AppRoutingModule { }
  