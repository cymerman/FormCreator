import {Component, Input, OnInit, ViewEncapsulation} from "@angular/core";
import {FormSettings} from "../../_models/form-settings.model";
import {FormHttpService} from "../../../shared-module/services/form-http.service";

@Component({
  selector: "form-component",
  templateUrl: "form.component.html",
  encapsulation: ViewEncapsulation.None
})
export class FormComponent implements OnInit {

  private _components: Array<FormComponent>;

  @Input()
  public formValues: object;

  @Input("components")
  set components(value) {
    this._components = value;
    if (value) {
      //this.prepareForm();
    }
  }
  constructor(formHttpService: FormHttpService) {

  }

  public ngOnInit(): void {

  }

}
