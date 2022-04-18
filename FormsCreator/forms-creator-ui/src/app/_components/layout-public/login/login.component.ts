import {Component, ElementRef, OnInit, ViewChild} from "@angular/core";
import {FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {CustomRequiredValidator} from "../../../_validators/custom-required.validator";
import {AuthService} from "../../../shared-module/services/auth.service";

@Component({
  selector: 'login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  public form: FormGroup;
  public showError: boolean = false;

  @ViewChild("signInButton")
  public signInButton: ElementRef | undefined;

  constructor(private fb: FormBuilder,
              private authService: AuthService) {

    this.form = this.fb.group({
      login: new FormControl(null, {validators: [CustomRequiredValidator.validate]}),
      password: new FormControl(null, {validators: [CustomRequiredValidator.validate]})
    });
  }

  public ngOnInit(): void {
  }

  public signIn() {
    if (!this.validate()) {
      return;
    }
    localStorage.clear();

    this.showError = false;
    this.authService.signIn(this.form.controls['login'].value, this.form.controls['password'].value)
      .subscribe(() => {
          this.authService.redirectToAfterLoginPage();
        },
        error => {
          this.showError = true;
        });
  }

  public validate(): boolean {
    if (this.signInButton && this.signInButton.nativeElement) {
      this.signInButton.nativeElement.focus()
    }

    this.form.controls['login'].markAsDirty();
    this.form.controls['password'].markAsDirty();
    this.form.controls['login'].markAsTouched();
    this.form.controls["password"].markAsTouched();

    return this.form.valid;
  }
}
