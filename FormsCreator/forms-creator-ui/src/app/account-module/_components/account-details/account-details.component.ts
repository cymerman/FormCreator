import {Component, OnInit} from "@angular/core";
import {AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, } from "@angular/forms";

import {CustomRequiredValidator} from "../../../_validators/custom-required.validator";
import {AccountDto} from "../../_models/account.model";
import {AccountService} from "../../account.service";
import {ToastrService} from "ngx-toastr";


@Component({
  selector: "account-details",
  templateUrl: "account-details.component.html"
})
export class AccountDetailsComponent implements OnInit {
  public user: AccountDto = new AccountDto();
  public isPasswordChanging: boolean = false;
  public form: FormGroup;

  constructor(private accountService: AccountService,
              private toastrService:ToastrService,
              private fb: FormBuilder) {

    this.form = this.fb.group({
      password: new FormControl(null, CustomRequiredValidator.validate),
      passwords: new FormGroup({
        newPassword: new FormControl(null, [CustomRequiredValidator.validate]),
        repeatNewPassword: new FormControl(null, CustomRequiredValidator.validate),
      }, AccountDetailsComponent.areNewPasswordsEqual)
    });
  }

  public ngOnInit(): void {
    this.accountService.get()
      .subscribe((claims) => {
        this.user = claims;
      }, () => {
        this.toastrService.info("Pobieranie danych użytkownika");
      });

  }

  public startChangingPassword() {
    this.isPasswordChanging = true;
  }

  public cancelChangingPassword() {
    this.form.reset();
    this.isPasswordChanging = false;
  }

  public changePassword() {
    if (!this.validate()) {
      return;
    }

    // let params = new ChangePasswordParams();
    // params.oldPassword = this.form.controls.password.value;
    // params.newPassword = this.form.controls.passwords["controls"].newPassword.value;
    //
    // this.accountService.changePassword(params)
    //   .subscribe(() => {
    //     this.messageService.publish(new Message("Zmiana hasła", "Zmiana hasła powiodła się.", MessageType.Success));
    //     this.form.reset();
    //     this.isPasswordChanging = false;
    //   }, error => {
    //     if (error && error.status === 400 && error.error) {
    //       if (error.error.code === "0001") {
    //         this.messageService.publish(new Message("Zmiana hasła", "Hasło nie spełnia wymagań polityki haseł.", MessageType.Error));
    //       }
    //       else if (error.error.code === "0007") {
    //         this.messageService.publish(new Message("Zmiana hasła", "Obecne hasło jest niepoprawne.", MessageType.Error));
    //       }
    //     } else if (error && error.status === 404) {
    //       this.messageService.publish(new Message("Zmiana hasła", "Nie znaleziono użytkownika", MessageType.Error));
    //     } else {
    //       this.messageService.publish(new Message("Zmiana hasła", this.labelsDict.shared.anErrorHasOccured, MessageType.Error));
    //     }
    //   });
  }

  private validate(): boolean {
    this.form.controls.password.markAsDirty();
    this.form.controls.passwords["controls"].newPassword.markAsDirty();
    this.form.controls.passwords["controls"].repeatNewPassword.markAsDirty();

    return this.form.valid;
  }

  private static areNewPasswordsEqual(c: AbstractControl): ValidationErrors | null {
    const password = c.value["newPassword"];
    const repeat = c.value["repeatNewPassword"];

    if (password === repeat) {
      return null;
    }

    return {
      areEqual: true
    };
  }
}
