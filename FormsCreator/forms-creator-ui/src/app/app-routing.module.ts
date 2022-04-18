import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {LayoutPublicComponent} from "./_components/layout-public/layout-public.component";
import {LayoutAppComponent} from "./_components/layout-app/layout-app.component";
import {LoginComponent} from "./_components/layout-public/login/login.component";
import {DashboardComponent} from "./_components/dashboard/dashboard.component";
import {AuthenticatedGuardService} from "./shared-module/services/authenticated-guard.service";
import {FormComponent} from "./form-builder-module/_components/form-component/form.component";

const routes: Routes = [
  {
    path: "",
    component: LayoutPublicComponent,
    children: [
      {
        path: "", component: LoginComponent
      }
    ]
  },
  {
    path: 's/:id', component: FormComponent
  },
  {
    path: "",
    canActivate: [AuthenticatedGuardService],
    component: LayoutAppComponent,
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent,
        data: {
          breadcrumb: "Pulpit"
        }
      },
      {
        path: "form-definition",
        data: {
          breadcrumb: ""
        },
        loadChildren: () => import('./form-definition-module/form-definition.module').then(m => m.FormDefinitionModule),
      },
      {
        path:"account",
        data: {
          breadcrumb: "Twoje konto"
        },
        loadChildren: () => import('./account-module/account.module').then(m => m.AccountModule),
      }
    ]
  },

  {path: '**', redirectTo: ''}
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
