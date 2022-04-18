export enum ColumnType {
  Text = 1,
  Decimal =2,
  Integer= 3,
  Date= 4,
  Bool =5
}

export class FilterItem {
  constructor() {
    this.operator = FilterOperator.Eq;
    this.value = null;
  }

  public operator: FilterOperator;
  public value: any;
  public fieldName: string = "";
  public display: string = "";
  public columnType: ColumnType = ColumnType.Text;
  public isValid?: boolean;
  public isCombo?: boolean;
  public values?: any[] = [];
}

export enum FilterOperator {
  Lt = 1,
  Gt,
  Eq,
  Lte,
  Gte,
  Sw,
  Ew,
  Ct
}

export class ActionButton {
  public text: string = "";
  // @ts-ignore
  public click: () => void;
}
