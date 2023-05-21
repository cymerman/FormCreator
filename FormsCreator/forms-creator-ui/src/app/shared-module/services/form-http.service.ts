import {Injectable} from "@angular/core";
import {HttpClient, HttpParams} from "@angular/common/http";
import {FormCreatorEnvironment} from "../models/form-creator-environment.model";
import {Observable} from "rxjs";
import {QueryResult} from "../models/queries.model";
import {FormDefinition} from "../../form-definition-module/_models/form-definition.model";
import { Form } from "src/app/form-builder-module/_models/form.model";

@Injectable({
  providedIn: 'root'
})

export class FormHttpService {
  private formControllerUrl = "Form/";
  constructor(private httpClient: HttpClient, private env: FormCreatorEnvironment) {
  }

  public query(params: HttpParams): Observable<QueryResult<Form>> {
    return this.httpClient.get<QueryResult<Form>>(this.formControllerUrl, {params: params});
  }

  public getForm(id:string) {
    return this.httpClient.get<any>(this.env.apiUrl + this.formControllerUrl + id);
  }

  public sendForm(formId:string,formData:string) {
    return this.httpClient.post(this.env.apiUrl + this.formControllerUrl + formId, formData);
  }
}
