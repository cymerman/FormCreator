import {HttpClient, HttpParams} from "@angular/common/http";
import {Injectable} from "@angular/core";
import * as moment from "moment";
import {environment} from "../../../environments/environment";
import {Observable} from "rxjs";
import {FormDefinition, FormDefinitionStatus} from "../_models/form-definition.model";
import {QueryResult} from "../../shared-module/models/queries.model";


@Injectable()
export class FormDefinitionService {
  protected url: string = environment.apiUrl + "FormDefinition";

  constructor(private httpClient: HttpClient) {
  }

  public query(params: HttpParams): Observable<QueryResult<FormDefinition>> {
    return this.httpClient.get<QueryResult<FormDefinition>>(this.url, {params: params});
  }

  public get(id: number): Observable<FormDefinition> {
    return this.httpClient.get<FormDefinition>(this.url + "/" + id);
  }

  public add(params: FormDefinition): Observable<number> {
    return this.httpClient.post<number>(this.url + "/", params);
  }

  public update(id: number, params: FormDefinition): Observable<number> {
    return this.httpClient.put<number>(this.url + "/" + id, params);
  }

  public delete(id: number): Observable<{}> {
    return this.httpClient.delete<{}>(this.url + "/" + id);
  }

  public updateStatus(id: number, status: FormDefinitionStatus) {
    return this.httpClient.put<number>(this.url + "/" + id + "/Status/" + status, {});
  }

  public copy(id: number) {
    return this.httpClient.post<number>(this.url + "/" + id + "/copy", {});
  }

  public searchByTitle(title:any, excludedIds:any) {
    return this.httpClient.post<FormDefinition>(this.url + "/Title/", {title: title, excludedIds: excludedIds});
  }

  public printApplicationDefinition(id: number, applicationTitle: string) {
    this.httpClient.get<any>(this.url + "/" + id + "/print", <any>{responseType: 'blob'})
      .subscribe(fileResult => {
        const fileName = applicationTitle + "_" + moment().format("DDMMYYYY").toString() + "." + "pdf";
        if (navigator.appVersion.toString().indexOf('.NET') > 0) {
          // @ts-ignore
          window.navigator.msSaveBlob(fileResult, fileName);
        }
        else {
          const link = document.createElement('a');
          // @ts-ignore
          link.href = window.URL.createObjectURL(fileResult);
          link.download = fileName;
          link.dispatchEvent(new MouseEvent(`click`, {bubbles: true, cancelable: true, view: window}));
        }
      }, error => {

      });
  }
}


