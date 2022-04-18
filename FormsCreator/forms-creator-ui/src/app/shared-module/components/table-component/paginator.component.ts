import {Component, Input, OnChanges, OnInit, SimpleChanges} from "@angular/core";
import {TableDirective} from "./table.directive";

@Component({
  selector: "paginator",
  templateUrl: "paginator.component.html"
})
export class PaginatorComponent implements OnInit, OnChanges {
  public activePage: number = 0;
  public rowsOnPage: number = 25;
  public lastPage: number = 0;
  public rowsOnPageSet = [15, 25, 50];

  @Input("paginatorDataLength")
  public dataLength: number = 0;

  public constructor(private table: TableDirective) {
  }

  public ngOnInit(): void {
    this.setPage(1);
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if (changes["dataLength"]) {
      this.lastPage = Math.ceil(this.dataLength / this.rowsOnPage);
      if (this.lastPage === 0){
        this.lastPage = 1;
      }

      if (this.activePage > this.lastPage) {
        this.activePage = this.lastPage;
      }
    }
  }

  public setPage(page: number): void {
    if (this.activePage === page) {
      return;
    }

    if (page < 1) {
      return;
    }

    if (page > this.lastPage && this.lastPage !== 0) {
      return;
    }

    this.activePage = page;
    this.table.setPage(page, this.rowsOnPage);
  }

  public setRowsOnPage(rowsOnPage: number): void {
    this.table.setPage(this.activePage, rowsOnPage);
  }
}
