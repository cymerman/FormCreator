import {Component, Input, OnInit} from "@angular/core";
import {FormGroup} from "@angular/forms";
import {FormComponent} from "../../../../_models/form-settings.model";
import {FormBuilderService} from "../../../../form-builder-service";

@Component({
  selector: "form-component-conditional-details",
  templateUrl: "form-component-conditional-details.component.html"
})
export class FormComponentConditionalDetailsComponent implements OnInit {

  @Input("formGroup")
  public formGroup: FormGroup;

  @Input("details")
  public details: FormComponent;

  @Input("formComponents")
  public formComponents: Array<FormComponent>;

  public fieldNames: Array<string>;

  constructor() {
  }

  public ngOnInit(): void {
    this.fieldNames = FormBuilderService.getFieldNames(this.formComponents, this.details.id);
  }
}
