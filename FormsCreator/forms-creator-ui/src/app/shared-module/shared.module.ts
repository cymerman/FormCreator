import {ModuleWithProviders, NgModule} from "@angular/core";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {TableDirective} from "./components/table-component/table.directive";
import {ColumnSorterComponent} from "./components/table-component/column-sorter.component";
import {CommonModule} from "@angular/common";
import {PaginatorComponent} from "./components/table-component/paginator.component";
import {ConfirmationComponent} from "./components/confirmation-component/confirmation.component";
import {PaginatorService} from "./services/paginator.service";

import {FilterItemComponent} from "./components/filter-item-component/filter-item.component";
import {ListFilterComponent} from "./components/list-filter/list-filter.component";
import {FilterOperatorTypeNamePipe} from "./services/filter-operator-type-name.pipe";
import {FormsModule} from "@angular/forms";
import {BsDropdownModule} from "ngx-bootstrap/dropdown";
import { BsDatepickerModule } from "ngx-bootstrap/datepicker";

import {TableService} from "./services/table.service";
import {ImgSrcDirective} from "./_directives/img-src.directive";
import {RequestInterceptorService} from "./services/request.interceptor";
import {AuthService} from "./services/auth.service";
import {AuthenticatedGuardService} from "./services/authenticated-guard.service";
import {TinyMCEComponent} from "./components/tinymce-component/tinymce.component";

@NgModule({
  imports: [
    HttpClientModule,
    CommonModule,
    FormsModule,
    BsDatepickerModule,
    BsDropdownModule
  ],
  declarations: [
    TableDirective,
    ColumnSorterComponent,
    PaginatorComponent,
    ConfirmationComponent,
    FilterItemComponent,
    ListFilterComponent,
    FilterOperatorTypeNamePipe,
    ImgSrcDirective,
    TinyMCEComponent
  ],
  exports: [
    TableDirective,
    ColumnSorterComponent,
    PaginatorComponent,
    ConfirmationComponent,
    FilterItemComponent,
    ListFilterComponent,
    FilterOperatorTypeNamePipe,
    ImgSrcDirective,
    TinyMCEComponent
  ],
  entryComponents: [
    ConfirmationComponent
  ],
  providers: [
    AuthService,
    AuthenticatedGuardService,
    PaginatorService,
    TableService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RequestInterceptorService,
      multi: true
    }
  ]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders<any> {
    return {
      ngModule: SharedModule,
      providers: [
        AuthService,
        AuthenticatedGuardService,
        PaginatorService,
        TableService,
      ]
    };
}}
