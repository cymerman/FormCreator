import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from "@angular/core";
import {FilterItem, FilterOperator} from "../../models/filters.model";
import * as moment from "moment";

@Component({
  selector: "filter-item",
  templateUrl: "filter-item.component.html"
})
export class FilterItemComponent implements OnInit {
  @Input()
  public item: FilterItem;

  @Output()
  public filterChanged: EventEmitter<{}>;

  @ViewChild("value")
  public value: any;

  constructor() {
    this.item = new FilterItem();
    this.filterChanged = new EventEmitter<{}>();
  }

  public selectOperator(op: FilterOperator) {
    this.item.operator = op;
    this.emitFilterChanged(this.value.valid);
  }

  public ngOnInit(): void {
    if (this.item.columnType === 4) {
      if (this.item.value) {
        this.item.value = moment(this.item.value, "YYYY-MM-DD").toDate();
      }
    }

    if (this.item.columnType === 5) {
      this.item.value = this.item.value === "true" ? true : this.item.value === "false" ? false : null;
    }
  }

  public emitFilterChanged(isValid: boolean) {
    this.item.isValid = isValid;
    this.filterChanged.emit();
  }
}
