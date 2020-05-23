import { Component } from '@angular/core';
import { LoadingService } from './services/loading-service/loading.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(
    private loadingService: LoadingService,
    public router: Router
  ) {
    this.loadingService.onLoadingStatusChange.subscribe((status) => {
      setTimeout(() => this.page.showLoading = status, 0);
    })
  }

  page = {
    showLoading: false as boolean
  }

}
