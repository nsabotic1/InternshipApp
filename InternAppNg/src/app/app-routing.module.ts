import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { ApplicationFormComponent } from "./application-form/application-form.component";
import { ApplicationComponent } from "./application/application.component";
import { LoginComponent } from "./login/login.component";
import { PublicwebsiteComponent } from "./publicwebsite/publicwebsite.component";
import { SelectionComponent } from "./selection/selection.component";
import { SidenavComponent } from "./sidenav/sidenav.component";
import { HeaderComponent } from "./header/header.component";
import { UsersComponent } from "./users/users.component";
import { DetailsSelectionComponent } from "./selection/details-selection/details-selection.component";
import { AddSelectionComponent } from "./selection/add-selection/add-selection.component";
import { EditSelectionComponent } from "./selection/edit-selection/edit-selection.component";

import { RoleGuard } from "./auth/role.guard";
import { AuthGuard } from "./auth/auth.guard";

const appRoutes: Routes = [

  {
    path: '', component: HeaderComponent,
    children: [ //Public routes
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: PublicwebsiteComponent },
      { path: 'apply', component: ApplicationFormComponent },
    ]
  },
  {
    path: 'dashboard', component: SidenavComponent, canActivate: [AuthGuard],
    children: [ //private routes
      { path: '', redirectTo: 'application', pathMatch: 'full' },
      { path: 'application', component: ApplicationComponent },
      { path: 'selection', component: SelectionComponent },
      { path: 'selection/:id', component: DetailsSelectionComponent },
      { path: 'add', component: AddSelectionComponent },
      { path: 'selection/edit/:id', component: EditSelectionComponent },
      { path: 'users', canActivate: [RoleGuard], component: UsersComponent }
    ]
  },

  { path: 'login', component: LoginComponent }
]

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
