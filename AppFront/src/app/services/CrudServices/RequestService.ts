import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from './Enviroment';
import { IAsset } from 'src/app/model/IAsset';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  private httpOptions: any = {};

  constructor(private http: HttpClient, private translate: TranslateService) {
  }
  updateHttpOptions() {
    this.httpOptions = {
      headers: new HttpHeaders({
        'Accept-Language': this.translate.currentLang
      })
    };
  }
  
  getListAssets(): Observable<any> {
    return this.http.get(enviroment.endpoint + enviroment.myApiUrl);
  }
  postAsset(asset: IAsset): Observable<any> {
    this.updateHttpOptions();
    return this.http.post(enviroment.endpoint + enviroment.myApiUrl, asset);
  }
  deleteAsset(id: string): Observable<any> {
    this.updateHttpOptions();
    return this.http.delete(enviroment.endpoint + enviroment.myApiUrl + id);
  }
  putAsset(name: string, asset: IAsset): Observable<any> {
    this.updateHttpOptions();
    return this.http.put(enviroment.endpoint + enviroment.myApiUrl + name, asset);
  }
  ValidMailSend(): Observable<any> {
    this.updateHttpOptions();
    return this.http.get(enviroment.endpoint + enviroment.mailApi);
  }
}
