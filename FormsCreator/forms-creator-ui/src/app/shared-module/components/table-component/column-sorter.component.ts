import {Component, Input, OnInit} from "@angular/core";
import {SortEvent, TableDirective} from "./table.directive";
import {SortDirection} from "../../models/queries.model";

@Component({
  selector: "column-sorter",
  templateUrl: "column-sorter.component.html"
})
export class ColumnSorterComponent implements OnInit {
  @Input()
  public fieldName: string = "";
  public sortDirection: SortDirection = SortDirection.None;

  constructor(private table: TableDirective) {
  }

  public ngOnInit(): void {
    this.table.onSortChange.subscribe((event: SortEvent) => {
      if (this.fieldName === event.fieldName) {
        this.sortDirection = event.direction;
      } else {
        this.sortDirection = SortDirection.None;
      }
    });
  }

  public sort() {
    if (this.sortDirection == SortDirection.None) {
      this.table.setSort(this.fieldName, SortDirection.Asc);
    } else if (this.sortDirection == SortDirection.Asc) {
      this.table.setSort(this.fieldName, SortDirection.Desc);
    } else if (this.sortDirection == SortDirection.Desc) {
      this.table.setSort(this.fieldName, SortDirection.Asc);
    }
  }
}


