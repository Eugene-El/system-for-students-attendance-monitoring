import { Injectable, Output, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  @Output() onLoadingStatusChange = new EventEmitter<boolean>();
  private loadingStatus : boolean = false;

  constructor() { }

  public addLoading() : void {
    this.loadingStatus = true;
    this.checkLoadingStatus();
  }

  public endLoading() : void {
    this.loadingStatus = false;
    this.checkLoadingStatus();
  }

  private checkLoadingStatus() : void {
    this.onLoadingStatusChange.emit(this.loadingStatus);
  }

}
