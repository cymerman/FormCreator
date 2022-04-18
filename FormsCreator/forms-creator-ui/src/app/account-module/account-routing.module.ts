import {NgModule} from "@angular/core";
import {RouterModule} from "@angular/router";
import {AccountDetailsComponent} from "./_components/account-details/account-details.component";

const routes = [
  {path: "", component: AccountDetailsComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule {
}

