import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { AssetServiceService } from 'src/app/services/webservices/asset-service.service';
import { IAsset } from 'src/app/model/IAsset';
import { ICreateServices } from 'src/app/services/interfaces/ICreateServices';
import { SharedDataService } from 'src/app/services/sharedataservices/SharedData';
import { IValidatorServices } from '../../services/interfaces/IValidatorServices';


@Component({
  selector: 'app-create-asset',
  templateUrl: './create-asset.component.html',
  styleUrls: ['./create-asset.component.css']
})

export class CreateAssetComponent implements OnInit {
  ngOnInit(): void {

  }
  IdForEdit = "";
  loading = false;
  loadingCountry = false;
  asset: IAsset | undefined;
  register: FormGroup;
  //departmentEdit: string = "";
  ViewEditButton: boolean = false;

  constructor(private _AssetServices: AssetServiceService, private formBuilder: FormBuilder, private toastr: ToastrService,
    private router: Router, private sharedData: SharedDataService, @Inject('ICreateServicesToken') private createServices: ICreateServices,
    private translateService: TranslateService, @Inject('IValidatorServicesToken') private validate: IValidatorServices) {


    this.register = this.formBuilder.group({
      id: "",
      assetName: ['', /*[Validators.required,*/ [this.validate.ValidName.bind(this)]],
      department: ['', /*[Validators.required,*/ [this.validate.ValidDepartment.bind(this)]],
      EMailAdressOfDepartment: ['', /*[Validators.required,*/ [this.validate.ValidMail.bind(this)]],
      PurchaseDate: ['', /*Validators.required,*/ this.validate.ValidatePurchaseDate.bind(this)],
      LifeSpan: ['', /*[Validators.required,*/ [this.validate.ValidateLifespanDate.bind(this)]]
    });

    this.sharedData.currentAssetData.subscribe(asset => {
      if (asset && asset.name != null) {
        this.ViewEditButton = true;
        this.selectedValue = asset.department;
        this.IdForEdit = asset.id;
        this.register.patchValue({
          id: asset.id,
          assetName: asset.name,
          EMailAdressOfDepartment: asset.departmentMail,
          department: this.selectedValue,
          //countryOfDepartment: asset.countryOfDepartment,
          PurchaseDate: this.createServices.FormattedDate(asset),
          LifeSpan: asset.lifespan,
        });
        console.log(asset);
      }
    });
  }
  async submit(): Promise<void> {
    this.loading = true;

    const asset: IAsset = {
      id: null,
      name: this.register.value.assetName,
      department: parseInt(this.register.value.department),
      departmentMail: this.register.value.EMailAdressOfDepartment,
      //countryOfDepartment: this.register.value.countryOfDepartment,
      purchaseDate: this.register.value.PurchaseDate,
      lifespan: this.register.value.LifeSpan,
      RemainingLifespan: {}
    }
    console.log(asset);
    try {
      const message = await this.createServices.AddAsset(asset);
      this.translateService.get('SaveAsset').subscribe((translatedMessage: string) => {
        this.toastr.success(message, translatedMessage);
      });
      this.router.navigate(["/app-assets-list"]);
    } catch (error) {
      this.toastr.error(error as string, "Error");
      console.log(error + " aqui es");
    }
    this.loading = false;
  }

  selectedValue: string = "";
  purchaseDateError: string = '';

  //onPurchaseDateBlur() {
  //  const purchaseDate = this.register.value.PurchaseDate;
  //  const oneYearAgo = new Date();
  //  oneYearAgo.setFullYear(oneYearAgo.getFullYear() - 1);

  //  if (purchaseDate < oneYearAgo.toISOString()) {
  //    this.translateService.get('PurchaseError').subscribe((translatedMessage: string) => {
  //      this.purchaseDateError = translatedMessage
  //    });

  //  } else {
  //    this.purchaseDateError = '';
  //  }
  //}
  
  PutAsset() {
    const asset: IAsset = {
      id: this.IdForEdit,
      name: this.register.value.assetName,
      department: parseInt(this.register.value.department),
      departmentMail: this.register.value.EMailAdressOfDepartment,
      //countryOfDepartment: this.register.value.countryOfDepartment,
      purchaseDate: this.register.value.PurchaseDate,
      lifespan: this.register.value.LifeSpan,
      RemainingLifespan: {}
    }
    console.log(this.IdForEdit);

    this._AssetServices.putAsset(this.IdForEdit, asset).subscribe(data => {
      this.toastr.info(data.message);

      this.ViewEditButton = false;
      this.router.navigate(["/app-assets-list"]);
    }, error => {
      this.toastr.error(error.error.message);
      
    })

  }
  cancelEdit() {
    //this.departmentEdit = '';
    this.ViewEditButton = false;
    this.register.reset;
    this.router.navigate(["/app-assets-list"]);
  }

  Reset() {
    this.register.reset();
    //this.departmentEdit = '';
  }
}
