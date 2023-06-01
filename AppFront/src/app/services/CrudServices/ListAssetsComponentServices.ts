import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { IAsset } from "src/app/model/IAsset";
import { RequestService } from "./RequestService";
import { IListServices } from "../interfaces/IListServices";

@Injectable({
  providedIn: 'root'
})

export class ListAssetsComponentServices implements IListServices {
  message: string = "";
  constructor(private _AssetServices: RequestService, private translateService: TranslateService) {
  }

  GetAssets(): Observable<IAsset[]> {
    return this._AssetServices.getListAssets();
  }

  async deleteAsset(id: string, name: string): Promise<string> {
    return new Promise<string>((resolve, reject) => {
      this._AssetServices.deleteAsset(id).subscribe(data => {

        this.translateService.get('Delete').subscribe((translatedMessage: string) => {
          this.message = translatedMessage + name;
          resolve(this.message);
        });
        
      }, error => {
        this.message = error;
        reject(error);
      });
    });
  }
}
