import { AbstractControl, ValidationErrors } from "@angular/forms";
import { IAsset } from "../../model/IAsset";

export interface ICreateServices {
  getDepartment(dpto: number): any;
  AddAsset(asset: IAsset): any;
  FormattedDate(asset: IAsset): any;
}
