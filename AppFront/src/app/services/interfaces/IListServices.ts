import { Observable } from "rxjs";
import { IAsset } from "../interfaces/model/IAsset";

export interface IListServices {
  GetAssets(): Observable<IAsset[]>;
  deleteAsset(assteName: string): any;
}
