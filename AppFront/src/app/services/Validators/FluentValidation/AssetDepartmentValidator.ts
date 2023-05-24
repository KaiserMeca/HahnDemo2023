import { Validator } from 'fluentvalidation-ts';

export interface Dpto {
  dpto: string;
}

export class AssetDepartmentValidator extends Validator<Dpto> {
  constructor() {
    super();

    this.ruleFor('dpto').notEmpty()
  }
}
