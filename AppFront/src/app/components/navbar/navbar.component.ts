import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AssetServiceService } from 'src/app/services/webservices/asset-service.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  activeLang: string = 'en-US';

  constructor(private translate: TranslateService, private assetService: AssetServiceService) {
  }
  changeLang(lang: string) {
    this.translate.use(lang);
    this.activeLang = lang;
    this.assetService.updateHttpOptions();
  }
}
