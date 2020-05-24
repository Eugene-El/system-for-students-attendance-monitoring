import { Component, OnInit } from '@angular/core';
import { AuthorizationModel } from './models/authorizationModel';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { AuthorizationDataService } from './services/authorization-data.service';
import { NotificationService } from 'src/app/services/notification-service/notification.service';
import { Router } from '@angular/router';
import { AuthorizationService } from 'src/app/services/authorization-service/authorization.service';

@Component({
  selector: 'app-authorization',
  templateUrl: './authorization.component.html',
  styleUrls: ['./authorization.component.css']
})
export class AuthorizationComponent implements OnInit {

  constructor(
    private loadingService: LoadingService,
    private notificationService: NotificationService,
    private authorizationDataService: AuthorizationDataService,
    private router: Router,
    private authorizationService: AuthorizationService
  ) { }

  ngOnInit() {
    this.loadingService.endLoading();
    this.methods.checkToken();
  }

  page = {
    authorizationModel: new AuthorizationModel("", ""),
    hidePassword: true as boolean
  }

  methods = {
    checkToken: () => {
      this.loadingService.addLoading(); 
      this.authorizationDataService.checkToken(this.authorizationService.getToken())
        .then((result) => {
          this.loadingService.endLoading();
          if (result)
            this.router.navigate(['home']);
        })
        .catch((error) => {
          this.notificationService.processError(error);
          this.loadingService.endLoading();
        });
    },
    login: () => {
      if (this.page.authorizationModel.login == "" || this.page.authorizationModel.password == "") {
        this.notificationService.fillAllFields();
        return;
      }

      this.loadingService.addLoading();
      this.authorizationDataService.authorize(this.page.authorizationModel)
        .then((user) => {
          this.loadingService.endLoading();
          this.authorizationService.setAuthorizationInfo(user.token, user.role);
          this.router.navigate(['home']);
        })
        .catch((error) => {
          this.notificationService.processError(error);
          this.loadingService.endLoading();
        });
    }
  }

}
