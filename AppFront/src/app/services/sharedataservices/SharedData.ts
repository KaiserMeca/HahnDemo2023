import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IAsset } from 'src/app/model/IAsset';

@Injectable({
  providedIn: 'root'
})
export class SharedDataService {
  private assetData = new BehaviorSubject<any>(null);
  currentAssetData = this.assetData;

  constructor() { }

  //This function is to pass the data obtained from assetList and pass it to CreateAsset for editing
  changeAssetData(asset: IAsset) {
    this.assetData.next(asset);
  }
}
