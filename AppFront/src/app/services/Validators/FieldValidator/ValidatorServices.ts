import { Injectable } from "@angular/core";
import { AbstractControl, ValidationErrors } from "@angular/forms";
import { IValidatorServices } from "../../interfaces/IValidatorServices";
import { AssetNameValidator } from "../FluentValidation/AssetNameValidator";
import { AssetMailValidator } from "../FluentValidation/AssetMailValidator";
import { AssetDepartmentValidator } from "../FluentValidation/AssetDepartmentValidator";
import { AssetLifeSpanValidator } from "../FluentValidation/AssetLifeSpanValidator";

@Injectable({
  providedIn: 'root'
})
export class ValidatorServices implements IValidatorServices {
  
  ValidName(control: AbstractControl): ValidationErrors | null {
    const fluentValidator = new AssetNameValidator();
    const validationResult = fluentValidator.validate({ name: control.value });
    return validationResult;
  }

  ValidMail(control: AbstractControl): ValidationErrors | null {
    const fluentValidator = new AssetMailValidator();
    const validationResult = fluentValidator.validate({ mail: control.value });
    return validationResult;
  }

  ValidatePurchaseDate(control: AbstractControl): ValidationErrors | null {
    const currentDate = new Date();
    const purchaseDate = new Date(control.value);
    const timeDiff = Math.abs(currentDate.getTime() - purchaseDate.getTime());
    const diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
    return diffDays > 365 ? { purchaseDateError: true } : null;
  }

  ValidDepartment(control: AbstractControl): ValidationErrors | null {

    const departmentValue = control.value.toString();
    const fluentValidator = new AssetDepartmentValidator();
    const validationResult = fluentValidator.validate({ dpto: departmentValue });
    return validationResult;
  }

  ValidateLifespanDate(control: AbstractControl): ValidationErrors | null {
    const fluentValidator = new AssetLifeSpanValidator();
    const validationResult = fluentValidator.validate({ lifeSpan: control.value });
    return validationResult;
  }
}
