import {NgModule} from "@angular/core";
import {RouterModule} from "@angular/router";
import {FormListComponent} from "./_components/form-list-component/form-list.component";

const routes = [
  {path: "list", component: FormListComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FormBuildingRoutingModule {
}

