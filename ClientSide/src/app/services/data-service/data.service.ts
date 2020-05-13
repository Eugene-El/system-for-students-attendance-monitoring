import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private apiUri: string;
  private initialized: boolean = false;

  constructor(
    private http: HttpClient,
    private translateService: TranslateService
  ) {
    this.http.get("assets/config/config.json").subscribe((config: any) => {
      if (config != null)
        this.apiUri = config.webApiBasePath;
      this.initialized = true;
    });
  }

  public get(path: string): Promise<any> {
    let headers = new HttpHeaders().set("language", this.translateService.currentLang);
    return new Promise((resolve, reject) => {
      this.waitTillInitialized().then(() => {
        this.http.get(this.mergePaths(this.apiUri, path), { headers: headers })
          .subscribe(val => { resolve(val as any) }, err => { reject(err) });
      });
    });
  }

  public post(path: string, model: object): Promise<any> {
    return new Promise((resolve, reject) => {
      this.waitTillInitialized().then(() => {
        this.http.post(this.mergePaths(this.apiUri, path), model)
          .subscribe(val => { resolve(val as any) }, err => { reject(err) });
      });
    });
  }

  public delete(path: string, id: string): Promise<any> {
    let params = new HttpParams().set("id", id.toString());
    return new Promise((resolve, reject) => {
      this.waitTillInitialized().then(() => {
      this.http.delete(this.mergePaths(this.apiUri, path), {
        params: params
      })
        .subscribe(val => { resolve(val as any) }, err => { reject(err) });
      });
    });
  }

  private mergePaths(path1: string, path2: string): string {
    if (path1.endsWith("/") && path2.startsWith("/"))
      return path1.slice(0, path1.length - 1) + path2;
    if (!path1.endsWith("/") && !path2.startsWith("/"))
      return path1 + "/" + path2;
    return path1 + path2;
  }

  private waitTillInitialized(): Promise<void> {
    return new Promise((resolve) => {
      if (this.initialized)
        resolve()
      else
        setTimeout(() => { resolve(this.waitTillInitialized()); }, 10);
    });
  }

}
