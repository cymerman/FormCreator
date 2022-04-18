import {NgModule} from "@angular/core";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {SharedModule} from "../shared-module/shared.module";
import {AccountDetailsComponent} from "./_components/account-details/account-details.component";
import {TabsModule} from "ngx-bootstrap/tabs";
import {TooltipModule} from "ngx-bootstrap/tooltip";
import {AccountRoutingModule} from "./account-routing.module";
import {AccountService} from "./account.service";

@NgModule({
  declarations: [
    AccountDetailsComponent
  ],
  exports: [

  ],
  imports: [
    AccountRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    SharedModule,
    CommonModule,
    TabsModule,
    TooltipModule,
  ],
  entryComponents: [

  ],
  providers: [
    AccountService
  ]
})
export class AccountModule {
}
