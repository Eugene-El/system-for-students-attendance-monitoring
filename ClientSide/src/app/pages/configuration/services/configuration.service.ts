import { Injectable } from '@angular/core';
import { ConfigurationModel } from '../models/configurationModel';
import { DataService } from 'src/app/services/data-service/data.service';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationService {

  constructor(private dataService: DataService) { }

  private paths = {
    get: 'api/Configuration/Get/',
    save: 'api/Configuration/Save/'
  }


  public get() : Promise<ConfigurationModel> {
    return this.dataService.get(this.paths.get);
  }

  public save(configuration: ConfigurationModel): Promise<void> {
    return this.dataService.post(this.paths.save, configuration);
  }

}
