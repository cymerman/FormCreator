import {AbstractControl, ValidationErrors} from "@angular/forms";


export class CustomRequiredValidator {
  private static isEmptyInputValue(value: any): boolean {
    // we don't check for string here so it also works with arrays
    return value == null || value.length === 0;
  }

  public static validate(c: AbstractControl): ValidationErrors | null {
    let text = c.value;

    if (text != null && text != undefined && (typeof text === 'string' || text instanceof String)) {
      text = text.toString().trim();
    }

    return CustomRequiredValidator.isEmptyInputValue(text) ? {'required': true} : null;
  }
}


