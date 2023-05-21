import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateAssetComponent } from '../components/createview/create-asset.component';
import { AssetsListComponent } from '../components/listview/assets-list.component'

const routes: Routes = [
  { path: '', redirectTo: '/app-create-asset', pathMatch: 'full' },
  { path: 'app-create-asset', component: CreateAssetComponent },
  { path: 'app-assets-list', component: AssetsListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
