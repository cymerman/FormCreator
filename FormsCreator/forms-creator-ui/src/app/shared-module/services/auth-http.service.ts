import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {FormCreatorEnvironment} from "../models/form-creator-environment.model";
import {SignIn} from "../models/auth/sign-in.model";

@Injectable({
  providedIn: 'root'
})

export class AuthHttpService {
  private authControllerUrl = "Auth/";

  constructor(private httpClient: HttpClient, private env: FormCreatorEnvironment) {
  }

  public signIn(login: string, password: string) {
    let signIn = new SignIn(password, login);
    return this.httpClient.post<any>(this.env.apiUrl + this.authControllerUrl + "sign-in", signIn);
  }

  public logout() {
    return this.httpClient.delete(this.env.apiUrl + this.authControllerUrl + "logout");
  }

  public isAuthenticated() {
    return this.httpClient.get<any>(this.env.apiUrl + this.authControllerUrl + "is-authenticated").toPromise();
  }
}
