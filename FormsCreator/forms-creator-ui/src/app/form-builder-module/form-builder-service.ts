import {FieldTypes, FormComponent} from "./_models/form-settings.model";

export class FormBuilderService {
  public static findElementByName(components: Array<FormComponent>, name: string): FormComponent | null {
    if (!components) {
      return null;
    }

    let result = null;
    for (const component of components) {
      // Test current object
      if (component.name === name) {
        return component;
      }

      if (component.isLayout) {
        if (component.type == FieldTypes.columns) {
          for (let column of component.columns) {
            result = this.findElementByName(column.childrens, name);
            if (result) {
              return result;
            }
          }
        }
        else {
          result = this.findElementByName(component.childrens, name);
          if (result) {
            return result;
          }
        }
      }
    }

    return null;
  }

  public static findElementById(components: Array<FormComponent>, id: string): FormComponent | null {
    if (!components) {
      return null;
    }

    let result = null;
    for (const component of components) {
      // Test current object
      if (component.id === id) {
        return component;
      }

      if (component.isLayout) {
        if (component.type == FieldTypes.columns) {
          for (let column of component.columns) {
            result = this.findElementById(column.childrens, id);
            if (result) {
              return result;
            }
          }
        }
        else {
          result = this.findElementById(component.childrens, id);
          if (result) {
            return result;
          }
        }
      }
    }

    return null;
  }

  public static getFieldNames(array: Array<FormComponent>, currentId: string): Array<string> {
    if (!array) {
      return [];
    }

    const result: Array<string> = [];
    for (let item of array) {
      if (item.isLayout) {
        if (item.type == FieldTypes.columns) {
          for (let column of item.columns) {
            let names = this.getFieldNames(column.childrens, currentId);
            for (let name of names) {
              result.push(name);
            }
          }
        } else {
          let names = this.getFieldNames(item.childrens, currentId);
          for (let name of names) {
            result.push(name);
          }
        }
      } else {
        if (item.name && item.id !== currentId) {
          result.push(item.name);
        }
      }
    }

    return result;
  }

}
