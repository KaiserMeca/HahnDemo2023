import { Validator } from 'fluentvalidation-ts';

export interface Name {
  name: string;
}

export class AssetNameValidator extends Validator<Name> {
  constructor() {
    super();

    this.ruleFor('name')
      .notEmpty().minLength(5)
  }
}



