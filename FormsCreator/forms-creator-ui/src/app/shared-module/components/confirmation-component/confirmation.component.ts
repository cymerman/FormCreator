import {Component, EventEmitter} from "@angular/core";
import { BsModalService,BsModalRef } from "ngx-bootstrap/modal";

@Component({
  selector: "confirmation-modal",
  templateUrl: "confirmation.component.html"
})
export class ConfirmationComponent {
  public onConfirm: EventEmitter<{}>;
  public title: string = "";
  public text: string = "";
  public height: string = "";

  constructor(public modalRef: BsModalRef) {
    this.onConfirm = new EventEmitter<{}>();
  }

  public confirm() {
    this.onConfirm.emit({});
    this.modalRef.hide();
  }
}
