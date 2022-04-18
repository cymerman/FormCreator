import {Component, EventEmitter, Input, OnInit, Output} from "@angular/core";
import {ActionButton, FilterItem} from "../../models/filters.model";

@Component({
  selector: "helper-list-filter",
  templateUrl: "list-filter.component.html"
})
export class ListFilterComponent implements OnInit {
  @Input()
  public filterItems: FilterItem[] = [];

  @Input()
  public addButtonVisible: boolean = false;

  @Input()
  public additionalButtons: ActionButton[] = [];

  @Output()
  public filterClick: EventEmitter<{}>;

  @Output()
  public addClick: EventEmitter<{}>;

  constructor() {
    this.filterClick = new EventEmitter<{}>();
    this.addClick = new EventEmitter<{}>();
  }

  public ngOnInit(): void {
    for (let item of this.filterItems) {
      (<any>item).origValue = item.value;
      (<any>item).origOperator = item.operator;
    }
  }

  public isFilterSet(): boolean {
    for (let item of this.filterItems) {
      if ((<any>item).origValue !== item.value) {
        return true;
      }
    }

    return false;
  }

  public clearFilter(): void {
    for (let item of this.filterItems) {
      item.value = (<any>item).origValue;
      item.operator = (<any>item).origOperator;
    }

    this.emitFilterClick();
  }

  public emitFilterClick(): void {
    this.filterClick.emit();
  }

  public emitAddClick(): void {
    this.addClick.emit();
  }
}
