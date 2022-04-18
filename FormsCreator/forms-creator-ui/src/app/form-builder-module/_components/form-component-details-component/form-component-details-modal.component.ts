import { BsModalRef } from "ngx-bootstrap/modal";
import {Component, EventEmitter, OnInit, ViewEncapsulation} from "@angular/core";
import {AbstractControl, FormArray, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Column, Conditional, FieldTypes, FormComponent, FormComponentStyles, FormSettings, Values} from "../../_models/form-settings.model";
import {FieldNameValidator} from "../../field-name.validator";
import * as _ from "lodash";
import {GuidService} from "../../guid.service";

@Component({
  selector: "form-component-details-modal",
  templateUrl: "form-component-details-modal.component.html",
  styleUrls: ["form-component-details-modal.component.css"],
  encapsulation: ViewEncapsulation.None
})
export class FormComponentDetailsModalComponent implements OnInit {
  private modal: BsModalRef;

  public onConfirm: EventEmitter<{}>;
  public onClose: EventEmitter<boolean>;
  public title: string = "";
  public details: FormComponent;
  public settings: FormSettings;
  public components: Array<FormComponent>;
  public date: Date;
  public isSubmitted: boolean = false;
  public form: FormGroup;
  public fieldTypes = FieldTypes;
  public isNew: boolean;

  constructor(public modalRef: BsModalRef,
              private fb: FormBuilder) {
    this.onConfirm = new EventEmitter<{}>();
    this.onClose = new EventEmitter<boolean>();
  }

  public get stylesGroup(){
    return this.form.get('styles') as FormGroup;
  }

  public get conditionalGroup(){
    return this.form.get('conditional') as FormGroup;
  }

  public get apiGroup(){
    return this.form.get('api') as FormGroup;
  }

  public getFormArray(arrayName:string): FormArray{
    return this.form.get(arrayName) as FormArray;
  }

  public getAbstractControlFromInnerArray(control:AbstractControl, controlName:string){
    let formArray = control as FormArray;
    return formArray.get(controlName);
  }

  public ngOnInit(): void {
    this.createForm();
  }

  public confirm() {
    if (this.isNew) {
      this.mapFormControl();
    } else {
      this.mapFromControlOnUpdate();
    }

    this.onConfirm.emit(this.details);
    this.modalRef.hide();
  }

  public save() {
    this.isSubmitted = true;
    if (!this.form.valid) {
      return;
    }

    this.confirm();
  }

  public isValuesFormVisible() {
    return this.details.type == FieldTypes.select ||
      this.details.type == FieldTypes.radio;
  }

  public close() {
    this.onClose.emit(this.isNew);
    this.modalRef.hide();
  }

  isNameVisible(): boolean {
    return !(this.details.type === FieldTypes.section
      || this.details.type === FieldTypes.columns
      || this.details.type === FieldTypes.img
      || this.details.type === FieldTypes.html);
  }

  public createForm() {
    if (!this.details.styles) {
      this.details.styles = new FormComponentStyles();
    }

    this.form = this.fb.group({
      name: [this.details.name, this.isNameVisible() ? [Validators.required, Validators.pattern("[a-z A-Z\\s_]+"), FieldNameValidator.validate(this.settings, this.details.id)] : []],
      label: [this.details.label, []],
      isLabelVisible: [this.details.isLabelVisible, []],
      placeholder: [this.details.placeholder, []],
      tooltip: [this.details.tooltip, []],
      src: [this.details.src, []],
      alt: [this.details.alt, []],
      desc: [this.details.desc, []],
      required: [this.details.validation.required, []],
      max: [this.details.validation.max, []],
      min: [this.details.validation.min, []],
      maxLength: [this.details.validation.maxLength, []],
      minLength: [this.details.validation.minLength, []],
      pattern: [this.details.validation.pattern, []],
      custom: [this.details.validation.custom, []],
      formula: [this.details.formula, []],
      css: [this.details.css, []],
      htmlContent: [this.details.htmlContent, []],

      columns: this.initColumns(this.details.columns),
      values: this.initValues(this.details.values),

      conditional: this.fb.group(
        {
          isVisible: [this.details.conditional.isVisible, []],
          relatedToName: [this.details.conditional.relatedToName, []],
          relatedToValue: [this.details.conditional.relatedToValue, []],
        }
      ),
      styles: this.fb.group(
        {
          componentClasses: [this.details.styles.componentClasses, []],
          labelClasses: [this.details.styles.labelClasses, []],
        }
      )
    });
  }

  public tinyMceKeyUpHandler(value:any, controlName:any) {
    if(controlName && this.form){
      this.form.get(controlName).setValue(value);
    }
  }

  public addColumn() {
    const control = (<FormArray>this.form.controls['columns']);
    let column = new Column();
    column.id = GuidService.generate();
    column.width = 6;
    column.offset = 0;
    this.details.columns.push(column);
    control.push(this.fb.group({
      id: [column.id],
      width: [column.width, Validators.required],
      offset: [column.offset],
    }));
  }

  public removeColumn(i: number) {
    const control = (<FormArray>this.form.controls['columns']);
    const id = control.at(i).value.id;
    control.removeAt(i);

    _.remove(this.details.columns,
      (el) => {
        return el.id === id;
      })
  }

  public addValue() {
    const control = (<FormArray>this.form.controls['values']);
    control.push(this.fb.group({
      value: ["", Validators.required],
      display: ["", Validators.required],
    }));

  }

  public removeValue(i: number) {
    const control = (<FormArray>this.form.controls['values']);
    control.removeAt(i);
  }

  public initValues(values: Array<Values>) {
    let resultFormArray = this.fb.array([]);

    for (let i = 0; i < values.length; i++) {
      let item = values[i];
      resultFormArray.push(this.fb.group({
        value: [item.value, [Validators.required]],
        display: [item.display, [Validators.required]],
      }))
    }

    return resultFormArray;
  }

  private initColumns(columns: Array<Column>) {
    if (this.details.columns.length == 0 && this.details.type == this.fieldTypes.columns) {
      const column = new Column();
      column.id = GuidService.generate();
      column.width = 6;
      column.offset = 0;

      this.details.columns.push(column);

      const column2 = new Column();
      column2.id = GuidService.generate();
      column2.width = 6;
      column2.offset = 0;
      this.details.columns.push(column2);
    }

    let resultFormArray = this.fb.array([]);

    for (let i = 0; i < columns.length; i++) {
      let item = columns[i];
      resultFormArray.push(this.fb.group({
        id: [item.id],
        width: [item.width, [Validators.required, Validators.min(0), Validators.max(12)]],
        offset: [item.offset, [Validators.required, Validators.min(0), Validators.max(12)]],
      }))
    }

    return resultFormArray;
  }

  private mapFromControlOnUpdate() {
    this.mapFormControlsSimple();
    const formModel = this.form.value;
    for (let i = 0; i < formModel.columns.length; i++) {
      for (let j = 0; j < this.details.columns.length; j++) {
        let formModelColumn = formModel.columns[i];
        let detailsColumn = this.details.columns[j];
        if (formModelColumn.id === detailsColumn.id) {
          detailsColumn.width = formModelColumn.width;
          detailsColumn.offset = formModelColumn.offset;
        }
      }
    }
  }

  private mapFormControl() {
    this.mapFormControlsSimple();

    const formModel = this.form.value;
    this.details.columns.length = 0;

    for (let i = 0; i < formModel.columns.length; i++) {
      let column = new Column();
      column.id = formModel.columns[i].id;
      column.offset = formModel.columns[i].offset;
      column.width = formModel.columns[i].width;
      this.details.columns.push(column);
    }
  }

  private mapFormControlsSimple() {
    const formModel = this.form.value;

    this.details.name = formModel.name as string;
    this.details.label = formModel.label as string;
    this.details.isLabelVisible = formModel.isLabelVisible as boolean;
    this.details.placeholder = formModel.placeholder as string;
    this.details.tooltip = formModel.tooltip as string;
    this.details.src = formModel.src as string;
    this.details.alt = formModel.alt as string;
    this.details.desc = formModel.desc as string;
    this.details.formula = formModel.formula as string;
    this.details.css = formModel.css as string;
    this.details.validation.required = formModel.required as boolean;
    this.details.validation.max = formModel.max as number;
    this.details.validation.min = formModel.min as number;
    this.details.validation.maxLength = formModel.maxLength as number;
    this.details.validation.minLength = formModel.minLength as number;
    this.details.validation.custom = formModel.custom as string;
    this.details.validation.pattern = formModel.pattern as string;
    this.details.htmlContent = formModel.htmlContent as string;

    this.details.styles = new FormComponentStyles();
    this.details.styles.componentClasses = formModel.styles.componentClasses as string;
    this.details.styles.labelClasses = formModel.styles.labelClasses as string;

    this.details.conditional = new Conditional();
    this.details.conditional.isVisible = formModel.conditional.isVisible;
    this.details.conditional.relatedToName = formModel.conditional.relatedToName;
    this.details.conditional.relatedToValue = formModel.conditional.relatedToValue;

    this.details.values.length = 0;
    for (let i = 0; i < formModel.values.length; i++) {
      let value = new Values();
      value.display = formModel.values[i].display;
      value.value = formModel.values[i].value;
      this.details.values.push(value);
    }
  }
}
