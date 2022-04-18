import {AfterViewInit, Directive, EventEmitter, Input, OnChanges, Output, SimpleChange} from "@angular/core";
import {RowsRange, SortDirection, Sorting} from "../../models/queries.model";
import {TableService} from "../../services/table.service";
import {ReplaySubject} from "rxjs";

@Directive({
  selector: "table[helperTable]",
  exportAs: "helperTable"
})
export class TableDirective implements OnChanges, AfterViewInit {
  @Input("helperTableOrderBy")
  public orderBy:string = "";
  @Input("helperTableOrderDirection")
  public orderDirection:SortDirection = SortDirection.Asc;
  @Input("helperTableServerPaging")
  public serverPaging: boolean = true;

  @Output("helperTableGetData")
  public getData: EventEmitter<GetDataEvent> = new EventEmitter<GetDataEvent>();
  public onSortChange: ReplaySubject<SortEvent> = new ReplaySubject<SortEvent>(1);

  private page: number = 0;
  private rowsOnPage: number = 0;

  constructor(private tableService: TableService) {
  }

  public ngOnChanges(changes: { [key: string]: SimpleChange }): any {
    if (changes["orderBy"] || changes["orderDirection"]) {
      if (this.orderBy) {
        this.onSortChange.next({
          fieldName: this.orderBy,
          direction: this.orderDirection
        });

        this.emitGetData();
      }
    }
  }

  public ngAfterViewInit(): void {
    this.tableService.setUpTopScroll();
  }

  public setSort(fieldName: string, direction: SortDirection): void {
    if (this.orderBy === fieldName && this.orderDirection === direction) {
      return;
    }

    this.orderBy = fieldName;
    // @ts-ignore
    this.orderDirection = direction;

    this.onSortChange.next({
      fieldName: fieldName,
      direction: direction
    });

    this.emitGetData();
  }

  public setPage(page: number, rowsOnPage: number): void {
    this.page = page;
    this.rowsOnPage = rowsOnPage;
    this.emitGetData()
  }

  public emitGetData() {
    if (this.orderBy === null
      || this.orderDirection === null
      || this.page === null
      || this.rowsOnPage === null) {

      return;
    }

    const event = {
      sorting: {
        fieldName: this.orderBy,
        direction: this.orderDirection
      },
      rowsRange: {
        skip: (this.page - 1) * this.rowsOnPage,
        take: this.rowsOnPage
      }
    };

    this.getData.emit(event);
  }

  public getEvent(): any {
    const event = {
      sorting: {
        fieldName: this.orderBy,
        direction: this.orderDirection
      },
      rowsRange: {
        skip: (this.page - 1) * this.rowsOnPage,
        take: this.rowsOnPage
      }
    };

    return event;
  }
}


export class SortEvent {
  public fieldName: string = "";
  public direction: SortDirection = SortDirection.Asc;
}

export class PageEvent {
  public activePage: number = 0;
  public rowsOnPage: number = 0;
  public dataLength: number = 0;
}

export class GetDataEvent {
  rowsRange?: RowsRange;
  sorting?: Sorting;
}

