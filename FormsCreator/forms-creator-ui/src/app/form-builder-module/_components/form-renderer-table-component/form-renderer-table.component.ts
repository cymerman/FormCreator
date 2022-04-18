import {Component, EventEmitter, Input, OnInit, Output} from "@angular/core";

import {FieldTypes, FormComponent} from "../../_models/form-settings.model";
import {FormArray, FormGroup} from "@angular/forms";

@Component({
  selector: "form-renderer-table",
  templateUrl: "form-renderer-table.component.html"
})
export class FormRendererTableComponent implements OnInit {
  @Input("components")
  public components: Array<FormComponent>;

  @Input("form")
  public form: FormGroup;

  @Output("addRow")
  public addRowEmitter: EventEmitter<string>;

  @Output("removeRow")
  public removeRowEmitter: EventEmitter<any>;

  @Input()
  public isSubmitted: boolean;

  @Input()
  public name: string;

  @Input()
  public formValues;

  public fieldTypes = FieldTypes;

  get namesControls(){
    return this.form.controls['name'] as FormArray;
  }

  public names(index: number, name: string) {
    let nameArray = this.form.controls['name'] as FormArray
    let indexedNameArray = nameArray.controls[index] as FormArray;
    return indexedNameArray.get(name);
  }

  constructor() {
    this.addRowEmitter = new EventEmitter<string>();
    this.removeRowEmitter = new EventEmitter<any>();
  }

  public ngOnInit() {
  }

  public addRow() {
    this.addRowEmitter.emit(this.name);
  }

  public removeRow(index) {
    this.removeRowEmitter.emit({name: this.name, index: index});
  }

  public evalFormula(formula: string, index) {
    if (!formula) {
      return;
    }

    formula = formula.replace(/{index}/g, index);
    eval(formula);
  }

  public getLabelClasses(component: FormComponent): string {
    if (!component.styles) {
      return "";
    }

    return component.styles.labelClasses;
  }
}
