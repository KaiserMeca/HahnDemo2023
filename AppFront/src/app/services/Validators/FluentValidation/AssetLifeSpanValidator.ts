import { Validator } from 'fluentvalidation-ts';

export interface LifeSpan {
  lifeSpan: number;
}

export class AssetLifeSpanValidator extends Validator<LifeSpan>{
  constructor() {
    super();

    this.ruleFor('lifeSpan').must((lifeSpan: number) => lifeSpan <= 20);
  }    
}
