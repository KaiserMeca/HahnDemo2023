import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IAsset } from 'src/app/services/interfaces/model/IAsset';
import { AssetServiceService } from 'src/app/services/webservices/asset-service.service';
import { SharedDataService } from '../../services/sharedataservices/SharedData';
import { IListServices } from '../../services/interfaces/IListServices';

@Component({
  selector: 'app-assets-list',
  templateUrl: './assets-list.component.html',
  styleUrls: ['./assets-list.component.css']
})
export class AssetsListComponent implements OnInit {
  ngOnInit(): void {
    this.GetAssets();
  }
  AssetsList: IAsset[] = [];
  AssetEdit: IAsset | undefined;
  selectId: string = "";
  selectName: string = "";

  constructor(private _AssetServices: AssetServiceService, private toastr: ToastrService, private router: Router,
    private sharedData: SharedDataService, @Inject('IListServicesToken') private listServices: IListServices) {
    this.sharedData.currentAssetData.next({ id: null });
  }

  async GetAssets() {
    const data = await this.listServices.GetAssets().toPromise();
    this.AssetsList = data as IAsset[];
    console.log(this.AssetsList);
  }

  async DeleteAsset() {
    this.toastr.success(await this.listServices.deleteAsset(this.selectId));
    this.GetAssets();
  }
  GetId(_id: string, _name: string) {
    this.selectId = _id
    this.selectName = _name;
  }

  //This function is to pass the data obtained from assetList and pass it to CreateAsset for editing
  Edit(asset: IAsset) {
    this.sharedData.changeAssetData(asset);
    this.router.navigate(["/app-create-asset"]);
  }

  getDepartment(dpto: number): string {
    switch (dpto) {
      case 0:
        return "HQ";
      case 1:
        return "Store1";
      case 2:
        return "Store2";
      case 3:
        return "Store3";
      case 4:
        return "MaintenanceStation";
      default:
        return "";
    }
  }
}
