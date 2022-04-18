import {NgModule} from "@angular/core";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {FormRendererComponent} from "./_components/form-renderer-component/form-renderer.component";
import {FormBuilderComponent} from "./_components/form-builder-component/form-builder.component";
import {FormComponentDetailsModalComponent} from "./_components/form-component-details-component/form-component-details-modal.component";
import {FormComponentValidationDetailsComponent} from "./_components/form-component-details-component/_components/form-component-validation-details/form-component-validation-details.component";
import {FormComponentConditionalDetailsComponent} from "./_components/form-component-details-component/_components/form-component-conditional-details/form-component-conditional-details.component";

import {FormBuilderComponentsContainerComponent} from "./_components/form-builder-components-container/form-builder-components-container.component";
import {FormRendererComponentsContainerComponent} from "./_components/form-renderer-component-container/form-renderer-components-container.component";
import {FormRendererPageComponent} from "./_components/form-renderer-page-component/form-renderer-page.component";
import {FormRendererTableComponent} from "./_components/form-renderer-table-component/form-renderer-table.component";
import {FormComponentStyleDetailsComponent} from "./_components/form-component-details-component/_components/form-component-style-details/form-component-style-details.component";
import {SafeHtmlPipe} from "./_pipes/safe-html.pipe";
import {ModalModule} from "ngx-bootstrap/modal";
import {BsDatepickerModule} from "ngx-bootstrap/datepicker";
import {AccordionModule} from "ngx-bootstrap/accordion";
import {TooltipModule} from "ngx-bootstrap/tooltip";
import {TabsModule} from "ngx-bootstrap/tabs";
import {SharedModule} from "../shared-module/shared.module";
import {NgxDnDModule} from "@swimlane/ngx-dnd";
import {FormComponent} from "./_components/form-component/form.component";

@NgModule({
  declarations: [
    FormRendererComponent,
    FormBuilderComponent,
    FormComponentDetailsModalComponent,
    FormBuilderComponentsContainerComponent,
    FormComponentValidationDetailsComponent,
    FormComponentConditionalDetailsComponent,
    FormComponentStyleDetailsComponent,
    FormRendererComponentsContainerComponent,
    FormRendererPageComponent,
    FormRendererTableComponent,
    FormComponent,
    SafeHtmlPipe,
  ],
  exports: [
    FormRendererComponent,
    FormBuilderComponent
  ],
  imports: [
    ReactiveFormsModule,
    FormsModule,
    SharedModule,
    CommonModule,
    ModalModule.forRoot(),
    NgxDnDModule,
    BsDatepickerModule,
    TabsModule,
    AccordionModule,
    TooltipModule
  ],
  entryComponents: [
    FormComponentDetailsModalComponent
  ],
  providers: [
  ]
})
export class FormBuilderModule {
}
