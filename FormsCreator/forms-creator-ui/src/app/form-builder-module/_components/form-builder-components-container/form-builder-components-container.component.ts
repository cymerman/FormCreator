import {Component, Input, ViewEncapsulation} from "@angular/core";

import {FieldTypes, FormComponent, FormSettings} from "../../_models/form-settings.model";

import {FormComponentDetailsModalComponent} from "../form-component-details-component/form-component-details-modal.component";
import { BsModalService,BsModalRef } from "ngx-bootstrap/modal";
import * as _ from "lodash";
import {GuidService} from "../../guid.service";
import {FormBuilderService} from "../../form-builder-service";

@Component({
  selector: "form-builder-components-container",
  templateUrl: "form-builder-components-container.component.html",
  styleUrls: ["form-builder-components-container.component.css"],
  encapsulation: ViewEncapsulation.None
})
export class FormBuilderComponentsContainerComponent {
  private modal: BsModalRef;

  @Input()
  public components: Array<FormComponent>;

  @Input()
  public formSettings: FormSettings;
  public fieldTypes = FieldTypes;

  constructor(protected modalService: BsModalService) {
  }

  public remove(id:any) {
    this.innerRemove(this.components, id);
  }

  private innerRemove(components: Array<FormComponent>, id:any): boolean | null {
    for (let i = 0; i < components.length; i++) {
      let component = components[i];
      if (component.id == id) {
        components.splice(i, 1);
        return true;
      }

      if (component.type == this.fieldTypes.columns) {
        for (let column of component.columns) {
          if (column.childrens.length > 0) {
            let result = this.innerRemove(column.childrens, id);
            if (result) {
              return null;
            }
          }
        }
      } else {
        if (component.childrens.length > 0) {
          let result = this.innerRemove(component.childrens, id);
          if (result) {
            return null;
          }
        }
      }
    }

    return null;
  }

  public handleDropEvent(event:any): void {
    let item = event.value;

    if (item.id) {
      return;
    }

    item.id = GuidService.generate();
    this.modal = this.modalService.show(FormComponentDetailsModalComponent,
      {
        class: "modal-lg",
        ignoreBackdropClick: true,
        initialState: {
          details: _.cloneDeep(item),
          settings: this.formSettings,
          components: this.components,
          isNew: true
        }
      });

    this.modal.content.onConfirm.subscribe((res:any) => {
      Object.assign(event.value, res);
    });

    this.modal.content.onClose.subscribe((res:any) => {
      if (res) {
        this.remove(item.id);
      }
    });
  }

  public edit(item:any) {
    this.modal = this.modalService.show(FormComponentDetailsModalComponent,
      {
        class: "modal-lg",
        ignoreBackdropClick: true,
        initialState: {
          details: _.cloneDeep(item),
          settings: this.formSettings,
          components: this.components,
          isNew: false
        }
      });

    this.modal.content.onConfirm.subscribe((res:any) => {
      let element = FormBuilderService.findElementById(this.components, res.id);
      Object.assign(element, res);
    });
  }
}
