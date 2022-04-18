import {ColumnType, FilterItem} from "../models/filters.model";
import {GetDataEvent, TableDirective} from "./table-component/table.directive";
import {HttpParams} from "@angular/common/http";
import {Directive, ViewChild} from "@angular/core";
import {SortDirection} from "../models/queries.model";
import * as moment from "moment";

@Directive()
export abstract class ListBaseComponent<TService> {
  // @ts-ignore
  public filters: FilterItem[];
  public totalRows: number = 0;
  public rows: Array<any> = [];

  @ViewChild("table")
  public table: TableDirective | undefined;

  constructor(protected service: TService) {
    this.createFilters();
  }

  public loadData(event: GetDataEvent) {
    (<any>this.service)
      .query(this.getParams(event))
      .subscribe((result:any) => {
          this.totalRows = result.total;
          this.rows = result.data;
        },
        () => {
          this.totalRows = 0;
          this.rows = [];
        });
  }

  public onFilterClick() {
    // @ts-ignore
    this.table.emitGetData();
  }

  // @ts-ignore
  protected abstract downloadItemText();

  // @ts-ignore
  protected abstract createFilters();

  protected getParams(event: GetDataEvent): HttpParams {
    let params = new HttpParams();
    if (event) {
      // @ts-ignore
      if (event.sorting.direction !== SortDirection.None) {
        // @ts-ignore
        params = params.set("sorting.fieldName", event.sorting.fieldName);
        // @ts-ignore
        params = params.set("sorting.direction", event.sorting.direction.toString());
      }

      // @ts-ignore
      params = params.set("rowsRange.skip", event.rowsRange.skip.toString());
      // @ts-ignore
      params = params.set("rowsRange.take", event.rowsRange.take.toString());
    }

    for (let i = 0; i < this.filters.length; i++) {
      if (this.filters[i].value !== null
        && this.filters[i].value !== undefined
        && this.filters[i].value !== "") {
        if (this.filters[i].columnType === ColumnType.Date) {
          params = params.set(this.filters[i].fieldName + ".value", moment(this.filters[i].value).format("YYYY-MM-DD"));
        }
        else {
          params = params.set(this.filters[i].fieldName + ".value", this.filters[i].value.toString());
        }
        params = params.set(this.filters[i].fieldName + ".operator", this.filters[i].operator.toString());
      }
    }

    return params;
  }
}
