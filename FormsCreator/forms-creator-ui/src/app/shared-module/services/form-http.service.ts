import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {FormCreatorEnvironment} from "../models/form-creator-environment.model";

@Injectable({
  providedIn: 'root'
})

export class FormHttpService {
  private formControllerUrl = "Form/";

  constructor(private httpClient: HttpClient, private env: FormCreatorEnvironment) {
  }

  public getForm(id:string) {
    return this.httpClient.get<any>(this.env.apiUrl + this.formControllerUrl + id).toPromise();
  }
}
