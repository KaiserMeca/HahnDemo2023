import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors } from '@angular/forms';
import { ICreateServices } from "src/app/services/interfaces/ICreateServices";
import { IAsset } from '../interfaces/model/IAsset';
import { AssetServiceService } from './asset-service.service';

@Injectable({
  providedIn: 'root'
})

export class CreateServices implements ICreateServices {

  message: string = "";

  constructor(private _AssetServices: AssetServiceService) {
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
  validatePurchaseDate(control: AbstractControl): ValidationErrors | null {
    const currentDate = new Date();
    const purchaseDate = new Date(control.value);
    const timeDiff = Math.abs(currentDate.getTime() - purchaseDate.getTime());
    const diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
    return diffDays > 365 ? { purchaseDateError: true } : null;
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
