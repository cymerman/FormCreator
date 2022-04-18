import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {TabsModule} from "ngx-bootstrap/tabs";
import {BsDropdownModule} from "ngx-bootstrap/dropdown";
import { TypeaheadModule } from "ngx-bootstrap/typeahead";
import { TooltipModule } from "ngx-bootstrap/tooltip";

import {FormDefinitionService} from "./_services/form-definition.service";
import {FormDefinitionRoutingModule} from "./form-definition-routing.module";
import {FormBuilderModule} from "../form-builder-module/form-builder.module";
import {FormDefinitionListComponent} from "./_components/form-definition-list/form-definition-list.component";
import {FormDefinitionDetailsComponent} from "./_components/form-definition-details/form-definition-details.component";
import {SharedModule} from "../shared-module/shared.module";


@NgModule({
  declarations: [
    FormDefinitionListComponent,
    FormDefinitionDetailsComponent
  ],
  imports: [
    FormDefinitionRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    BsDropdownModule,
    TabsModule,
    TooltipModule,
    FormBuilderModule,
    TypeaheadModule,
    SharedModule
  ],
  exports: [],
  providers: [
    FormDefinitionService
  ]
})
export class FormDefinitionModule {
}

