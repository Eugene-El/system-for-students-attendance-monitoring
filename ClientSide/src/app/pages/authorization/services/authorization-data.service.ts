import { Injectable } from '@angular/core';
import { DataService } from 'src/app/services/data-service/data.service';
import { AuthorizationModel } from '../models/authorizationModel';
import { AuthorizedUser } from '../models/authorizedUser';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationDataService {

  constructor(private dataService: DataService) { }

  private paths = {
    authorize: 'api/Authorization/Authorize/',
    checkToken: 'api/Authorization/CheckToken/'
  }

  public authorize(authorizationModel: AuthorizationModel) : Promise<AuthorizedUser> {
    return this.dataService.post(this.paths.authorize, authorizationModel);
  }

  public checkToken(token: string) : Promise<boolean> {
    return this.dataService.post(this.paths.checkToken, { token: token });
  }
}
