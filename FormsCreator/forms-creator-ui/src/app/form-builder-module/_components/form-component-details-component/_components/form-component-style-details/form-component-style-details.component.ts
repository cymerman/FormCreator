import {Component, Input, OnInit} from "@angular/core";
import {FormGroup} from "@angular/forms";
import {FormComponent} from "../../../../_models/form-settings.model";

@Component({
  selector: "form-component-style-details",
  templateUrl: "form-component-style-details.component.html"
})
export class FormComponentStyleDetailsComponent implements OnInit {

  @Input("formGroup")
  public formGroup: FormGroup;
  @Input("details")
  public details: FormComponent;

  constructor() {
  }

  public ngOnInit(): void {
  }

}
