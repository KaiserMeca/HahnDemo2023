import { HttpClientModule, HttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//Modules
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
// import ngx-translate and the http loader
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

//Components
import { AppComponent } from './app.component';
import { CreateAssetComponent } from '../components/createview/create-asset.component';
import { NavbarComponent } from '../components/navbar/navbar.component';
import { AssetsListComponent } from '../components/listview/assets-list.component';
import { LoadingComponent } from '../components/loading/loading.component';
import { ListServices } from '../services/webservices/ListServices';
import { CreateServices } from '../services/webservices/CreateSevices';

@NgModule({
  declarations: [
    AppComponent,
    CreateAssetComponent,
    NavbarComponent,
    AssetsListComponent,
    LoadingComponent,
  ],
  imports: [
    BrowserModule, HttpClientModule, ReactiveFormsModule, AppRoutingModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    FormsModule,
    // ngx-translate and the loader module
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [
    {
      provide: 'IListServicesToken',
      useClass: ListServices
    },
    {
      provide: 'ICreateServicesToken',
      useClass: CreateServices
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
// required for AOT compilation
export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json ');
}
