import {Component} from "@angular/core";
import { BsModalService,BsModalRef } from "ngx-bootstrap/modal";
import {Router} from "@angular/router";
import {ListBaseComponent} from "../../../shared-module/components/list-base.component";
import {FormDefinitionService} from "../../_services/form-definition.service";
import {ConfirmationComponent} from "../../../shared-module/components/confirmation-component/confirmation.component";
import {FormDefinitionDict, FormDefinitionStatus} from "../../_models/form-definition.model";
import {ColumnType, FilterOperator} from "../../../shared-module/models/filters.model";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: "form-definition-list",
  templateUrl: "form-definition-list.component.html"
})
export class FormDefinitionListComponent extends ListBaseComponent<FormDefinitionService> {
  private modal: BsModalRef;

  constructor(service: FormDefinitionService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private router: Router) {
    super(service);
  }

  public delete(id: number) {
    this.modal = this.modalService.show(ConfirmationComponent);
    this.modal.content.title = "Potwierdź usunięcie";
    this.modal.content.text = "Czy na pewno usunąć definicje formularza?";
    this.modal.content.onConfirm.subscribe(() => {
      this.service.delete(id)
        .subscribe(() => {
          this.toastr.success("Usuwanie definicji formularza", "Usuwanie definicji formularza powiodło się.");
          // @ts-ignore
          this.table.emitGetData();
        }, error => {
          if (error) {
            if (error.status === 404) {
              this.toastr.error("Usuwanie definicji formularza", "Nie znaleziono definicji formularza ID=" + id);
            } else if (error.status === 400 && error.error && error.error.code === "0035") {
              this.toastr.error("Usuwanie definicji formularza", "Brak możliwości usunięcia definicji wniosku w obecnym statusie");
            }
          } else {
            this.toastr.error("Usuwanie definicji formularza", "Wystąpił błąd");
          }
        });
    });
  }

  public onAddClick() {
    this.router.navigate(["/", "form-definition", "details"]);
  }

  protected downloadItemText() {
    return "Pobieranie formularzy";
  }

  public getStatusText(status: FormDefinitionStatus) {
    return FormDefinitionDict.getStatusText(status);
  }

  public copy(id: number) {
    this.service.copy(id).subscribe(res => {
      this.router.navigate(["/", "form-definition", "details", res]);
    }, err => {
      if (err) {
        if (err.status === 404) {
          this.toastr.error("Kopiowanie definicji formularza", "Nie znaleziono definicji formularza ID=" + id);
        }
      } else {
        this.toastr.error("Kopiowanie definicji formularza", "Wystąpił błąd");
      }
    })
  }

  protected createFilters() {
    const statuses = [{text: "Razem", value: null}];
    for (let i = 0; i < FormDefinitionDict.statuses.length; i++) {
      statuses.push({text: FormDefinitionDict.statuses[i].value, value: FormDefinitionDict.statuses[i].key});
    }

    this.filters = [];
    this.filters.push({
        fieldName: "title",
        operator: FilterOperator.Ct,
        value: null,
        columnType: ColumnType.Text,
        display: "Tytuł",
        isValid: true
      }, {
        fieldName: "description",
        operator: FilterOperator.Ct,
        value: null,
        columnType: ColumnType.Text,
        display: "Opis",
        isValid: true
      }, {
        fieldName: "name",
        operator: FilterOperator.Ct,
        value: null,
        columnType: ColumnType.Text,
        display: "Nazwa",
        isValid: true
      },
      {
        fieldName: "status",
        operator: FilterOperator.Eq,
        value: null,
        columnType: ColumnType.Integer,
        display: "Status",
        isValid: true,
        isCombo: true,
        values: statuses
      });
  }
}
