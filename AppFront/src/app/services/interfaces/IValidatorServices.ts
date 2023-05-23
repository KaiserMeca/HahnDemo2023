import { AbstractControl, ValidationErrors } from "@angular/forms";

export interface IValidatorServices {
  ValidName(control: AbstractControl): ValidationErrors | null;
  ValidMail(control: AbstractControl): ValidationErrors | null;
  validatePurchaseDate(control: AbstractControl): ValidationErrors | null;
}
