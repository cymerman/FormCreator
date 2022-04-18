import {AfterViewChecked, ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation} from "@angular/core";

import {Conditional, FieldTypes, FormComponent} from "../../_models/form-settings.model";
import {FormGroup} from "@angular/forms";

@Component({
  selector: "form-renderer-components-container",
  templateUrl: "form-renderer-components-container.component.html",
  styleUrls: ["form-renderer-components-container.component.css"],
  encapsulation: ViewEncapsulation.None
})
export class FormRendererComponentsContainerComponent implements OnInit, AfterViewChecked {
  @Input("components")
  public components: Array<FormComponent>;

  @Input("form")
  public form: FormGroup;

  @Input()
  public isSubmitted: boolean;

  @Output("addRow")
  public addRowEmitter: EventEmitter<string>;

  @Output("removeRow")
  public removeRowEmitter: EventEmitter<any>;

  public fieldTypes = FieldTypes;

  constructor(private cdRef: ChangeDetectorRef) {
    this.addRowEmitter = new EventEmitter<string>();
    this.removeRowEmitter = new EventEmitter<any>();
  }

  public ngOnInit() {
  }

  public ngAfterViewChecked() {
    this.cdRef.detectChanges();
  }

  public getLabelClasses(component: FormComponent): string {
    if (!component.styles) {
      return "";
    }

    return component.styles.labelClasses;
  }

  public getComponentClasses(component: FormComponent): string {
    if (!component.styles) {
      return "";
    }

    return component.styles.componentClasses;
  }

  public isComponentConditionalAndVisible(component: FormComponent): boolean {
    if (!component.conditional || !Conditional.isAnySet(component.conditional)) {
      return true;
    }


    var control = this.form.controls[component.conditional.relatedToName];
    if (!control) {
      return true;
    }

    if (control.value == component.conditional.relatedToValue) {
      if (component.conditional.isVisible) {
        this.enableOrDisableControl(component, true); // enable
      } else {
        this.enableOrDisableControl(component, false); // disable
      }

      return component.conditional.isVisible;
    } else {
      if (!component.conditional.isVisible) {
        this.enableOrDisableControl(component, true); // enable
      } else {
        this.enableOrDisableControl(component, false); // disable
      }

      return !component.conditional.isVisible;
    }
  }

  private enableOrDisableControl(component: FormComponent, isEnabled: boolean) {

    if (!component.isLayout) {
      if (component.type == FieldTypes.html) {
        return;
      }
      let slaveControl = this.form.controls[component.name];
      if (!slaveControl) {
        return;
      }
      if (isEnabled) {
        slaveControl.enable();
      } else {
        slaveControl.disable();
      }

    } else {
      if (component.childrens.length > 0) {
        this.enableOrDisableControlInner(component.childrens, isEnabled);
      } else if (component.columns.length > 0) {
        for (let column of component.columns) {
          this.enableOrDisableControlInner(column.childrens, isEnabled);
        }
      }
    }
  }

  private enableOrDisableControlInner(components: Array<FormComponent>, isEnabled: boolean) {
    if (!components) {
      return;
    }

    for (let i = 0; i < components.length; i++) {
      let component = components[i];
      if (component.type == FieldTypes.html) {
        continue;
      }

      if (!component.isLayout) {
        let slaveControl = this.form.controls[component.name];
        if (!slaveControl) {
          return;
        }
        if (isEnabled) {
          slaveControl.enable();
        } else {
          slaveControl.disable();
        }

      } else {
        if (component.childrens.length > 0) {
          this.enableOrDisableControlInner(component.childrens, isEnabled);
        } else if (component.columns.length > 0) {
          for (let column of component.columns) {
            this.enableOrDisableControlInner(column.childrens, isEnabled);
          }
        }
      }
    }
  }

  public addRowHandler(event) {
    this.addRowEmitter.emit(event);
  }

  public removeRowHandler(event) {
    this.removeRowEmitter.emit(event);
  }

  public evalFormula(formula) {
    if (!formula) {
      return;
    }

    eval(formula);
  }

  public selectChanged(component: FormComponent) {
    this.evalFormula(component.formula);
  }

  public getSelectValues(component: FormComponent) {
    return component.values;
  }
}
