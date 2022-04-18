import {Injectable} from "@angular/core";

export class PaginatorService {
  public paginate<T>(input: Array<T>, skip: number, take: number): Array<T> {
    const pageSize = take;
    if (input.length < skip) {
      return [];
    }

    let endIndex = skip + take;
    if (endIndex > input.length) {
      endIndex = input.length;
    }

    const result = [];
    for (let i = skip; i < endIndex; i++) {
      result.push(input[i]);
    }

    return result;
  }
}
