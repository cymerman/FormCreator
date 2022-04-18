import {Component, Input, OnInit, ViewEncapsulation} from "@angular/core";
import {FieldTypes, FormComponent, FormPage, FormSettings} from "../../_models/form-settings.model";
import { BsModalService,BsModalRef } from "ngx-bootstrap/modal";
import {ConfirmationComponent} from "../../../shared-module/components/confirmation-component/confirmation.component";

@Component({
  selector: "form-builder",
  templateUrl: "form-builder.component.html",
  styleUrls: ["form-builder.component.css"],
  encapsulation: ViewEncapsulation.None
})
export class FormBuilderComponent implements OnInit {
  public layoutComponents: Array<FormComponent> = [];
  public inputComponents: Array<FormComponent> = [];

  @Input("formSettings")
  public formSettings: FormSettings;

  private modal: BsModalRef;

  constructor(private modalService: BsModalService) {
    if (!this.formSettings) {
      this.formSettings = new FormSettings();
    }

    this.formSettings = new FormSettings();
    this.layoutComponents.push(new FormComponent("Sekcja", "oi-folder", FieldTypes.section, "Sekcja", true));
    this.layoutComponents.push(new FormComponent("Kolumny", "oi-ellipses", FieldTypes.columns, "Kolumny", true));
    this.layoutComponents.push(new FormComponent("Tabela", "oi-grid-three-up", FieldTypes.table, "Tabela", true));

    this.inputComponents.push(new FormComponent("Tekst", "oi-terminal", FieldTypes.text, "Tekst"));
    this.inputComponents.push(new FormComponent("Numer", "oi-elevator", FieldTypes.number, "Numer"));
    this.inputComponents.push(new FormComponent("Text area", "oi-text", FieldTypes.textArea, "Text area"));
    this.inputComponents.push(new FormComponent("Checkbox", "oi-task", FieldTypes.checkBox, "Checkbox"));
    this.inputComponents.push(new FormComponent("Data", "oi-calendar", FieldTypes.date, "Data"));
    this.inputComponents.push(new FormComponent("Obrazek", "oi-image", FieldTypes.img, "Obrazek"));
    this.inputComponents.push(new FormComponent("E-mail", "oi-envelope-closed", FieldTypes.email, "E-mail"));
    this.inputComponents.push(new FormComponent("Pole wyboru", "oi-list", FieldTypes.select, "Pole wyboru"));
    this.inputComponents.push(new FormComponent("Radio", "oi-target", FieldTypes.radio, "Radio"));
    this.inputComponents.push(new FormComponent("Html", "oi-code", FieldTypes.html, "Element html", false));
  }

  public builderDrag(e: any) {
  }

  public ngOnInit(): void {
  }

  public onWizardChange(event:any) {
    let previousSelection = event.target.value == "true";
    setTimeout(() => {
      this.formSettings.isWizard = !previousSelection;
    }, 1);

    this.modal = this.modalService.show(ConfirmationComponent);
    this.modal.content.title = "Zmiana typu formularza";
    this.modal.content.text = "Czy na pewno zmienić typ formularza ? Usunie to niezapisane zmiany.";
    this.modal.content.onConfirm.subscribe(() => {
      this.formSettings.isWizard = previousSelection;
      if (this.formSettings.isWizard) {
        this.formSettings.components.length = 0;
        const page = new FormPage();
        page.title = "Krok";
        page.index = 1;
        this.formSettings.pages.push(page)

      } else {
        this.formSettings.pages.length = 0;
      }
    });
  }

  public addPage() {
    let newPageNumer = this.formSettings.pages.length + 1;
    let page = new FormPage();
    page.title = "Krok ";
    page.index = newPageNumer;
    (<any>page).removable = true;

    this.formSettings.pages.push(page);
  }

  public removeTabHandler(index:any) {
    for (let i = index; i < this.formSettings.pages.length; i++) {
      this.formSettings.pages[i].index--;
    }

    this.formSettings.pages.splice(index, 1);
  }
}
