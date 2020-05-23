import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
import { AuthorizationService } from '../authorization-service/authorization.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private apiUri: string;
  private initialized: boolean = false;

  constructor(
    private http: HttpClient,
    private translateService: TranslateService,
    private authorizationService: AuthorizationService,
    private router: Router
  ) {
    this.http.get("assets/config/config.json").subscribe((config: any) => {
      if (config != null)
        this.apiUri = config.webApiBasePath;
      this.initialized = true;
    });
  }

  public get(path: string): Promise<any> {
    return new Promise((resolve, reject) => {
      this.waitTillInitialized().then(() => {
        this.http.get(this.mergePaths(this.apiUri, path), { headers: this.getHeaders() })
          .subscribe(val => { resolve(val as any) }, err => this.processError(err, reject));
      });
    });
  }

  public post(path: string, model: object): Promise<any> {
    return new Promise((resolve, reject) => {
      this.waitTillInitialized().then(() => {
        this.http.post(this.mergePaths(this.apiUri, path), model, { headers: this.getHeaders() })
          .subscribe(val => { resolve(val as any) }, err => this.processError(err, reject));
      });
    });
  }

  public delete(path: string, id: string): Promise<any> {
    let params = new HttpParams().set("id", id.toString());
    return new Promise((resolve, reject) => {
      this.waitTillInitialized().then(() => {
      this.http.delete(this.mergePaths(this.apiUri, path), {
        params: params,
        headers: this.getHeaders()
      })
        .subscribe(val => { resolve(val as any) }, err => this.processError(err, reject));
      });
    });
  }

  private processError (err, reject) {
    if (err != null && err.error != null && err.error.Message == "401 - Unauthorized")
    {
      this.router.navigate(['login']);
      reject({ error: "AUTHORIZATION.UNAUTHORIZED" });
    }
    reject(err);
  }

  private mergePaths(path1: string, path2: string): string {
    if (path1.endsWith("/") && path2.startsWith("/"))
      return path1.slice(0, path1.length - 1) + path2;
    if (!path1.endsWith("/") && !path2.startsWith("/"))
      return path1 + "/" + path2;
    return path1 + path2;
  }

  private getHeaders() {
    let headers = new HttpHeaders()
      .set("language", this.translateService.currentLang)
      .set("token", this.authorizationService.getToken());
    return headers;
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
