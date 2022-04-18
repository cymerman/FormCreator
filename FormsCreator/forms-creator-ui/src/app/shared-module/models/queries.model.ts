import {HttpParams} from "@angular/common/http";

export class QueryResult<T> {
  data: Array<T>;

  total: number;
}

export enum SortDirection {
  None = 0, Asc = 1, Desc = 2
}

export class Sorting {
  fieldName: string;

  direction: SortDirection;
}

export class RowsRange {
  skip: number;

  take: number;
}

export interface IQuery {
  rowsRange: RowsRange;
  sorting: Sorting;

  toParams(): HttpParams;
}
