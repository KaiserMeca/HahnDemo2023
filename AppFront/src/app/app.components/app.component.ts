import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { TranslateService } from "@ngx-translate/core";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  langs: string[] = [];

  constructor(http: HttpClient, private translate: TranslateService) {
    this.translate.setDefaultLang('en-US');
    this.translate.use('en-US');
    this.translate.addLangs(['en-US', 'es-ES']);
    this.langs = this.translate.getLangs();
  }

  title = 'WepAppFront';
}
