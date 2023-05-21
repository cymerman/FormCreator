import {Component} from "@angular/core";
import {Router} from "@angular/router";
import {ListBaseComponent} from "../../../shared-module/components/list-base.component";
import {ColumnType, FilterOperator} from "../../../shared-module/models/filters.model";
import {FormHttpService} from "../../../shared-module/services/form-http.service";

@Component({
  selector: "form-list",
  templateUrl: "form-list.component.html"
})
export class FormListComponent extends ListBaseComponent<FormHttpService> {

  constructor(service: FormHttpService) {
    super(service);
  }

  protected downloadItemText() {
    return "Pobieranie formularzy";
  }

  protected createFilters() {
    this.filters = [];
    this.filters.push({
        fieldName: "formUid",
        operator: FilterOperator.Ct,
        value: null,
        columnType: ColumnType.Text,
        display: "Id definicji formularza",
        isValid: true
      });
  }
}
