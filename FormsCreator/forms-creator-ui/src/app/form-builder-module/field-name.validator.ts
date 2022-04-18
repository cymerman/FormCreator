import {AbstractControl, ValidationErrors} from "@angular/forms";
import {FieldTypes, FormComponent, FormSettings} from "./_models/form-settings.model";

export class FieldNameValidator {

  public static validate(formSettings: FormSettings, currentId: string): ValidationErrors | null {
    return (c: AbstractControl) => {
      let name = c.value;

      if (!name) {
        return null;
      }

      if (!formSettings.isWizard) {
        return this.isNameDecalred(formSettings.components, name, currentId);
      }

      let result: null;
      for (let page of formSettings.pages) {
        result = this.isNameDecalred(page.components, name, currentId);
        if (result) {
          return result;
        }
      }

      return result;
    }
  }

  private static isNameDecalred(array: Array<FormComponent>, text: string, currentId: string) {
    let result = null;
    for (let item of array) {
      if (item.isLayout) {
        if (item.type == FieldTypes.columns) {
          for (let column of item.columns) {
            result = this.isNameDecalred(column.childrens, text, currentId);
            if (result) {
              return result;
            }
          }

        } else {
          result = this.isNameDecalred(item.childrens, text, currentId);
          if (result) {
            return result;
          }
        }
      } else {
        if (item.name == text && item.id != currentId) {
          return {nameAlreadyExists: true}
        }
      }
    }

    return result;
  }
}
