import { AbstractControl, ValidationErrors } from "@angular/forms";

export interface IValidatorServices {
  ValidName(control: AbstractControl): ValidationErrors | null;
  ValidMail(control: AbstractControl): ValidationErrors | null;
  ValidDepartment(control: AbstractControl): ValidationErrors | null;
  ValidatePurchaseDate(control: AbstractControl): ValidationErrors | null;
  ValidateLifespanDate(control: AbstractControl): ValidationErrors | null;
}
