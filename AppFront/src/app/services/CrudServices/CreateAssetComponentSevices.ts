import { Injectable } from '@angular/core';
import { ICreateServices } from "src/app/services/interfaces/ICreateServices";
import { IAsset } from '../../model/IAsset';
import { RequestService } from './RequestService';

@Injectable({
  providedIn: 'root'
})

export class CreateAssetComponentSevices implements ICreateServices {

  message: string = "";

  constructor(private _AssetServices: RequestService) {
  }
  async AddAsset(asset: IAsset): Promise<string> {
    return new Promise<string>((resolve, reject) => {
      this._AssetServices.postAsset(asset).subscribe(data => {
        this.message = data.message;
        resolve(this.message);
      }, error => {
        this.message = error.error.message;
        reject(this.message);
      });
    });
  }

  FormattedDate(asset: IAsset) {
    const purchaseDateConvert = new Date(asset.purchaseDate);
    const year = purchaseDateConvert.getFullYear();
    const month = purchaseDateConvert.getMonth() + 1;
    const day = purchaseDateConvert.getDate();
    const formattedDate = `${year}-${month.toString().padStart(2, '0')}-${day.toString().padStart(2, '0')}`;
    return formattedDate;
  }

  async getDepartment(dpto: number) {
    switch (dpto) {
      case 0:
        return 'HQ';

      case 1:
        return 'Store1';

      case 2:
        return 'Store2';

      case 3:
        return 'Store3';

      case 4:
        return 'MaintenanceStation';

      default:
        return 'default';
    }
  }
}
