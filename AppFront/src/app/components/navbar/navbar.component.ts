import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { RequestService } from 'src/app/services/CrudServices/RequestService';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  activeLang: string = 'en-US';

  constructor(private translate: TranslateService, private assetService: RequestService) {
  }
  changeLang(lang: string) {
    this.translate.use(lang);
    this.activeLang = lang;
    this.assetService.updateHttpOptions();
  }
}
