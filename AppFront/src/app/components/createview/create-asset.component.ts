import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { AssetServiceService } from 'src/app/services/webservices/asset-service.service';
import { IAsset } from 'src/app/services/interfaces/model/IAsset';
import { ICreateServices } from 'src/app/services/interfaces/ICreateServices';
import { SharedDataService } from 'src/app/services/sharedataservices/SharedData';

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
  departmentEdit: string = "";
  ViewEditButton: boolean = false;

  constructor(private _AssetServices: AssetServiceService, private formBuilder: FormBuilder, private toastr: ToastrService,
    private router: Router, private sharedData: SharedDataService, @Inject('ICreateServicesToken') private createServices: ICreateServices,
    private translateService: TranslateService) {

    this.register = this.formBuilder.group({
      id: 0,
      assetName: ['', [Validators.minLength(5), Validators.required]],
      department: [0, [Validators.nullValidator, Validators.required]],
      EMailAdressOfDepartment: ['', [Validators.email, Validators.required]],
      countryOfDepartment: ['', [Validators.required]],
      PurchaseDate: ['', [Validators.nullValidator, Validators.required, this.createServices.validatePurchaseDate]],
      LifeSpan: 3//new(?)
    });

    this.sharedData.currentAssetData.subscribe(asset => {
      if (asset && asset.assetName != null) {
        this.ViewEditButton = true;
        this.selectedValue = asset.department;
        this.IdForEdit = asset.assetName;
        this.register.patchValue({
          id: asset.id,
          assetName: asset.assetName,
          EMailAdressOfDepartment: asset.eMailAdressOfDepartment,
          countryOfDepartment: asset.countryOfDepartment,
          PurchaseDate: this.createServices.FormattedDate(asset),
          Lifespan: this.asset?.Lifespan,//new
          broken: asset.broken
        });
      }
    });
  }

  inputInvalidName = () => {
    const input = this.register.get('assetName');
    return input?.invalid && input.touched;
  };
  inputInvalidEmail = () => {
    const input = this.register.get('EMailAdressOfDepartment');
    return input?.invalid && input.touched;
  };
  //inputInvalidCoutry = () => {
  //  const input = this.register.get('countryOfDepartment');
  //  return this.validCountry != "" && input?.touched;
  //};
  inputInvalidPurchaseDate = () => {
    const input = this.register.get('PurchaseDate');
    return input?.invalid && input?.touched;
  };
  inputInvalidLifeSpan = () => {
    const input = this.register.get('assetName');
    return input?.invalid && input.touched;
  };

  async submit(): Promise<void> {
    this.loading = true;

    const asset: IAsset = {
      //Id: "",
      Name: this.register.value.assetName,
      Department: parseInt(this.register.value.department),
      DepartmentMail: this.register.value.EMailAdressOfDepartment,
      //countryOfDepartment: this.register.value.countryOfDepartment,
      PurchaseDate: this.register.value.PurchaseDate,
      //broken: this.register.value.broken,
      Lifespan: this.register.value.LifeSpan,//fix here (recibe el dato del html)
      RemainingLifespan: {}
    }
    console.log(JSON.stringify(asset) + "asset creado");

    try {
      const message = await this.createServices.AddAsset(asset);
      this.translateService.get('SaveAsset').subscribe((translatedMessage: string) => {
        this.toastr.success(message, translatedMessage);
      });
      this.router.navigate(["/app-assets-list"]);
    } catch (error) {
      this.toastr.error(error as string, "Error");
    }
    this.loading = false;
  }

  //onCountry() {
  //  this.loadingCountry = true;
  //  const countryIn = this.register.value.countryOfDepartment;
  //  this._AssetServices.searchCountry(countryIn).subscribe(data => {
  //    this.validCountry = "";
  //    this.loadingCountry = false;
  //  }, error => {
  //    console.log(error);
  //    this.validCountry = error.error.message;
  //    this.loadingCountry = false;
  //  })
  //}

  validCountry: string = "";
  selectedValue: string = "";
  purchaseDateError: string = '';

  onPurchaseDateBlur() {
    const purchaseDate = this.register.value.PurchaseDate;
    const oneYearAgo = new Date();
    oneYearAgo.setFullYear(oneYearAgo.getFullYear() - 1);

    if (purchaseDate < oneYearAgo.toISOString()) {
      this.translateService.get('PurchaseError').subscribe((translatedMessage: string) => {
        this.purchaseDateError = translatedMessage
      });

    } else {
      this.purchaseDateError = '';
    }
  }
  PutAsset() {
    const asset: IAsset = {
      //Id: "",//Obtener Id para editar
      Name: this.register.value.assetName,
      Department: parseInt(this.register.value.department),
      DepartmentMail: this.register.value.EMailAdressOfDepartment,
      //countryOfDepartment: this.register.value.countryOfDepartment,
      PurchaseDate: this.register.value.PurchaseDate,
      //broken: this.register.value.broken,
      Lifespan: this.register.value.LifeSpan,//fix here (recibe el dato del html)
      RemainingLifespan: {}
    }

    this._AssetServices.putAsset(this.IdForEdit, asset).subscribe(data => {
      this.toastr.success(data.message);
      this.ViewEditButton = false;
      this.router.navigate(["/app-assets-list"]);
    }, error => {
      this.toastr.error(error.error.message);
    })

  }
  cancelEdit() {
    this.departmentEdit = '';
    this.ViewEditButton = false;
    this.register.reset;
    this.router.navigate(["/app-assets-list"]);
  }

  Reset() {
    this.departmentEdit = '';
  }
}
