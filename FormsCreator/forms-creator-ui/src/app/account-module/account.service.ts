import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";
import {AccountDto} from "./_models/account.model";


@Injectable()
export class AccountService {
  protected url: string = environment.apiUrl + "Account";

  constructor(private httpClient: HttpClient) {
  }

  public get(): Observable<AccountDto> {
    return this.httpClient.get<AccountDto>(this.url);
  }
}


