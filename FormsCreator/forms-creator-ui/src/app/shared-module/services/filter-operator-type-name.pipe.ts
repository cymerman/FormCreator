import {Pipe, PipeTransform} from "@angular/core";
import {FilterOperator} from "../models/filters.model";

@Pipe({name: 'filterOperatorName'})
export class FilterOperatorTypeNamePipe implements PipeTransform {
  transform(value: FilterOperator): string {
    switch (value) {
      case FilterOperator.Lt:
        return "<";
      case FilterOperator.Gt:
        return ">";
      case FilterOperator.Lte:
        return "<=";
      case FilterOperator.Gte:
        return ">=";
      case FilterOperator.Eq:
        return "=";
      case FilterOperator.Sw:
        return "Zaczyna się";
      case FilterOperator.Ew:
        return "Kończy się";
      case FilterOperator.Ct:
        return "Zawiera";
      default:
        return "";
    }
  }
}
