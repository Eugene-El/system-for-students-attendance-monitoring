import { Injectable } from '@angular/core';
import { Role } from './role.enum';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  constructor(
    private router: Router
  ) { }


  public getToken(): string {
    let token = localStorage.getItem("token");
    return token == null ? "" : token;
  }

  public setAuthorizationInfo(token: string, role: Role) {
    localStorage.setItem("token", token);
    localStorage.setItem("role", role.toString());
  }

  public logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("role");
    this.router.navigate(['login']);
  }

  public isWorker(): boolean {
    let role = +localStorage.getItem("role");
    return role == Role.Worker;
  }

  public isLecturer(): boolean {
    let role = +localStorage.getItem("role");
    return role == Role.Lecturer;
  }

  public isWorkerOrLecturer(): boolean {
    return this.isWorker() || this.isLecturer();
  }

  public isStudent(): boolean {
    let role = +localStorage.getItem("role");
    return role == Role.Student;
  }

}
