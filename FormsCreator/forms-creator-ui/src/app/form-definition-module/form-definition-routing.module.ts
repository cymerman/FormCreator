import {NgModule} from "@angular/core";
import {RouterModule} from "@angular/router";
import {FormDefinitionDetailsComponent} from "./_components/form-definition-details/form-definition-details.component";
import {FormDefinitionListComponent} from "./_components/form-definition-list/form-definition-list.component";

const routes = [
  {path: "", redirectTo: "list", pathMatch: "full"},
  {path: "list", component: FormDefinitionListComponent},
  {path: "details", component: FormDefinitionDetailsComponent},
  {path: "details/:id", component: FormDefinitionDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FormDefinitionRoutingModule {
}

