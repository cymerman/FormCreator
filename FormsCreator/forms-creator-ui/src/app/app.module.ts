import {LOCALE_ID, NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";

import { AppComponent } from './app.component';
import {environment} from "../environments/environment";
import { ToastrModule } from 'ngx-toastr';
import {LayoutPublicComponent} from "./_components/layout-public/layout-public.component";
import {LayoutAppComponent} from "./_components/layout-app/layout-app.component";
import {CollapseModule} from "ngx-bootstrap/collapse";
import {AppRoutingModule} from "./app-routing.module";
import {LoginComponent} from "./_components/layout-public/login/login.component";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {TooltipModule} from "ngx-bootstrap/tooltip";
import {DashboardComponent} from "./_components/dashboard/dashboard.component";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {SharedModule} from "./shared-module/shared.module";
import {RequestInterceptorService} from "./shared-module/services/request.interceptor";
import {FormCreatorEnvironment} from "./shared-module/models/form-creator-environment.model";
import {AccountModule} from "./account-module/account.module";
import {FormComponent} from "./form-builder-module/_components/form-component/form.component";
import {FormBuilderModule} from "./form-builder-module/form-builder.module";

@NgModule({
  declarations: [
    AppComponent,

    LayoutPublicComponent,
    LoginComponent,

    LayoutAppComponent,
    DashboardComponent,
    FormComponent
  ],
  imports: [
    SharedModule.forRoot(),
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    AccountModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    FormBuilderModule
  ],
  providers: [
    {provide: LOCALE_ID, useValue: "pl"},
    {provide: FormCreatorEnvironment, useValue: environment},
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RequestInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
