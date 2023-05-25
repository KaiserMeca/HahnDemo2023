import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { IAsset } from "src/app/model/IAsset";
import { AssetServiceService } from "./asset-service.service";
import { IListServices } from "../interfaces/IListServices";

@Injectable({
  providedIn: 'root'
})

export class ListServices implements IListServices {
 /* AssetsList: IAsset[] = [];*/
  message: string = "";
  constructor(private _AssetServices: AssetServiceService, private translateService: TranslateService) {
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
