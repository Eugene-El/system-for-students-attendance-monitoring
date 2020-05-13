import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateLoader, TranslateModule, TranslateService, MissingTranslationHandler, MissingTranslationHandlerParams } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { AppComponent } from './app.component';
import { MenuComponent } from './menu/menu.component';
import { AppRoutingModule } from './routing/app-routing.module';
import { HomeComponent } from './pages/home/home.component';
import { FacultiesListComponent } from './pages/faculties/faculties-list/faculties-list.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

// Handle no translation case
export class CustomMissingTranslationHandler implements MissingTranslationHandler {
   handle(params: MissingTranslationHandlerParams) {
      const keyParts = params.key.split(".");
      return keyParts.pop();
   }
}

@NgModule({
   declarations: [
      AppComponent,
      MenuComponent,
      HomeComponent,
      FacultiesListComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      TranslateModule.forRoot({
         loader: {
         provide: TranslateLoader,
         useFactory: httpTranslateLoader,
         deps: [HttpClient]
         },
         missingTranslationHandler: {provide: MissingTranslationHandler, useClass: CustomMissingTranslationHandler},
      }),
      BrowserAnimationsModule,
      MatButtonModule,
      MatIconModule,
      MatTableModule,
      MatPaginatorModule,
      MatSortModule,
      MatProgressSpinnerModule
   ],
   providers: [
      {
         provide: APP_INITIALIZER,
         useFactory: appInitializerFactory,
         deps: [TranslateService],
         multi: true
      },
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }

// Find all localizations
export function httpTranslateLoader(http: HttpClient) {
   return new TranslateHttpLoader(http, "./assets/i18n/", ".json");
}

// Wait for localizations load
export function appInitializerFactory(translateService: TranslateService) {
   return () => new Promise<any>((resolve: any) => {
      translateService.addLangs(["en-GB", "ru-RU", "lv-LV"]);
      let language = localStorage.getItem("Language");
      if (!translateService.langs.includes(language))
      {
         language = "en-GB";
         localStorage.setItem("Language", language);
      }
      translateService.use(language).subscribe(() => {}, () => {},
         () => { resolve(null); });
   });
 }