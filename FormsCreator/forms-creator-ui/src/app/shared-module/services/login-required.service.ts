import {
  EventEmitter,
  Injectable
} from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class LoginRequiredService {
  private _loginRequired: EventEmitter<void> = new EventEmitter<void>();

  public get loginRequired() {
    return this._loginRequired;
  }

  public emit() {
    this._loginRequired.emit();
  }
}
