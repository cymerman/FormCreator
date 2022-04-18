import {Component, Input, OnInit} from "@angular/core";
import {FormGroup} from "@angular/forms";
import {FieldTypes, FormComponent} from "../../../../_models/form-settings.model";

@Component({
  selector: "form-component-validation-details",
  templateUrl: "form-component-validation-details.component.html"
})
export class FormComponentValidationDetailsComponent implements OnInit {

  @Input("formGroup")
  public formGroup: FormGroup;
  @Input("details")
  public details: FormComponent;

  constructor() {
  }

  public ngOnInit(): void {
  }

  public isMaxMinVisible(): boolean {
    return this.details.type === FieldTypes.number;
  }

  public isLengthAndPatternVisible(): boolean {
    return this.details.type === FieldTypes.text || this.details.type === FieldTypes.textArea;
  }
}
