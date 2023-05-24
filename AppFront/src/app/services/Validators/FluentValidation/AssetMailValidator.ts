import { Validator } from 'fluentvalidation-ts';

export interface Mail {
  mail: string;
}

export class AssetMailValidator extends Validator<Mail> {
  constructor() {
    super();
    
    this.ruleFor('mail')
      .emailAddress();
  }
}
