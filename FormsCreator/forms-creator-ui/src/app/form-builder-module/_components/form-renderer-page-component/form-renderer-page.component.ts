import {Component, Input, OnInit} from "@angular/core";

import {AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators} from "@angular/forms";
import { FieldTypes, FormComponent} from "../../_models/form-settings.model";

@Component({
  selector: "form-renderer-page",
  templateUrl: "form-renderer-page.component.html"
})
export class FormRendererPageComponent implements OnInit {

  private _components: Array<FormComponent>;

  public form: FormGroup;
  public fieldTypes = FieldTypes;
  public isSubmitted: boolean = false;

  @Input()
  public formValues: object;

  @Input("components")
  set components(value) {
    this._components = value;
    if (value) {
      this.prepareForm();
    }
  }

  get components() {
    return this._components;
  }

  constructor(private fb: FormBuilder) {
  }

  public ngOnInit(): void {
  }

  public prepareForm(): void {
    let group = this.fb.group({});
    this.prepareFormInner(this.components, group);
    this.form = group;
    this.setFormValues();
  }

  public prepareFormInner(array: Array<FormComponent>, formGroup: FormGroup) {
    if (!array) {
      return;
    }

    //for (let component of array)
    for (let i = 0; i < array.length; i++) {
      let component = array[i];
      if (component.isLayout) {
        if (component.type == FieldTypes.columns) {
          for (let column of component.columns) {
            this.prepareFormInner(column.childrens, formGroup);
          }
        }
        else if (component.type == FieldTypes.table) {
          let group = this.fb.group({});
          this.prepareFormInner(component.childrens, group);
          const arr = this.fb.array([]);
          arr.push(group);
          formGroup.addControl(component.name, arr);
        }
        else {
          this.prepareFormInner(component.childrens, formGroup);
        }
      } else {
        if (component.name) {
          let validators = [];
          if (component.type == this.fieldTypes.email) {
            validators.push(this.customEmailValidator);
          }

          if (component.validation) {
            if (component.validation.required) {
              if (component.type === this.fieldTypes.checkBox) {
                validators.push(Validators.requiredTrue);
              } else {
                validators.push(Validators.required);
              }
            }

            if (component.validation.maxLength) {
              validators.push(Validators.maxLength(component.validation.maxLength));
            }

            if (component.validation.minLength) {
              validators.push(Validators.minLength(component.validation.minLength));
            }

            if (component.validation.pattern) {
              validators.push(Validators.pattern(component.validation.pattern));
            }

            if (component.validation.min) {
              validators.push(Validators.min(component.validation.min));
            }

            if (component.validation.max) {
              validators.push(Validators.max(component.validation.max));
            }
          }

          formGroup.addControl(component.name, new FormControl(null, validators));
        }
      }
    }
  }

  public save() {
    this.validateForm();
  }

  public addRowHandler(event) {
    const control = (<FormArray>this.form.controls[event]);
    let tableComponent = this.findElementByName(this.components, event);
    let group = this.fb.group({});
    this.prepareFormInner(tableComponent.childrens, group);
    control.push(group);
  }

  public removeRowHandler(event) {
    const control = (<FormArray>this.form.controls[event.name]);
    if (control.length <= 1) {
      return;
    }

    control.removeAt(event.index);
  }

  public validateForm(): boolean {
    this.isSubmitted = true;

    return this.form.valid;
  }

  public isFormValid(): boolean {
    return this.validateForm();
  }

  public getFormValues() {
    return this.form.value;
  }

  public reloadFormValues(formData) {
    this.formValues = formData;
    this.setFormValues();
  }

  private setFormValues() {
    if (!this.formValues) {
      return;
    }

    for (const property in this.formValues) {
      if (!this.formValues.hasOwnProperty(property)) {
        continue;
      }

      let fc = this.form.controls[property];
      if (!fc) {
        continue;
      }

      if (Array.isArray(this.formValues[property])) {
        fc = this.fb.array([]);
        let tableComponent = this.findElementByName(this.components, property);

        for (let item of this.formValues[property]) {
          let fg = this.fb.group({});

          for (let itemProperty in item) {
            if (!item.hasOwnProperty(itemProperty)) {
              continue;
            }

            for (let component of tableComponent.childrens) {
              if (itemProperty == component.name) {
                if (component.name) {
                  let validators = [];

                  if (component.validation) {
                    if (component.validation.required) {
                      validators.push(Validators.required);
                    }

                    if (component.validation.maxLength) {
                      validators.push(Validators.maxLength(component.validation.maxLength));
                    }

                    if (component.validation.minLength) {
                      validators.push(Validators.minLength(component.validation.minLength));
                    }

                    if (component.validation.pattern) {
                      validators.push(Validators.pattern(component.validation.pattern));
                    }

                    if (component.validation.min) {
                      validators.push(Validators.min(component.validation.min));
                    }

                    if (component.validation.max) {
                      validators.push(Validators.max(component.validation.max));
                    }
                  }

                  let fc = new FormControl(null, validators);
                  fc.setValue(item[itemProperty]);
                  fg.addControl(component.name, new FormControl(item[itemProperty], validators));
                }
              }
            }
          }

          (<FormArray>fc).push(fg);
        }

        this.form.removeControl(property);
        this.form.addControl(property, fc);
      } else {

        var component = this.findElementByName(this.components, property);
        fc.setValue(this.formValues[property]);
      }
    }
  }

  private findElementByName(components: Array<FormComponent>, name: string): FormComponent | null {
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

  private customEmailValidator(control: AbstractControl): ValidationErrors | null {
    if (!control.value) {
      return null;
    }

    return Validators.email(control);
  }
}
