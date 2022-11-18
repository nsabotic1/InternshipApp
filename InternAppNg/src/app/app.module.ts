// Angular imports
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { AppRoutingModule } from './app-routing.module';
// Components
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { AppInfoComponent } from './application/app-info/app-info.component';
import { ApplicationFormComponent } from './application-form/application-form.component';
import { PublicwebsiteComponent } from './publicwebsite/publicwebsite.component';
import { ApplicationComponent } from './application/application.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { LoginComponent } from './login/login.component';
import { SelectionComponent } from './selection/selection.component';
import { DetailsSelectionComponent } from './selection/details-selection/details-selection.component';
import { UsersComponent } from './users/users.component';
import { AddUserModalComponent } from './users/add-user-modal/add-user-modal.component';
import { SelectionCommentInfoComponent } from './selection/selection-comment-info/selection-comment-info.component';
import { AddSelectionComponent } from './selection/add-selection/add-selection.component';
import { EditSelectionComponent } from './selection/edit-selection/edit-selection.component';
import { SpinnerComponent } from './spinner/spinner.component';
//Material
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialogModule } from '@angular/material/dialog';
//Interceptors
import { HttpErrorInterceptor } from './http-error-interceptor.service';
import { TokenInterceptorService } from './auth/token-interceptor.service';
import { IsAdminDirective } from './auth/is-admin.directive';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    ApplicationFormComponent,
    PublicwebsiteComponent,
    ApplicationComponent,
    SidenavComponent,
    AppInfoComponent,
    LoginComponent,
    SelectionComponent,
    SelectionCommentInfoComponent,
    UsersComponent,
    SpinnerComponent,
    AddUserModalComponent,
    DetailsSelectionComponent,
    AddSelectionComponent,
    EditSelectionComponent,
    IsAdminDirective
  ],
  imports: [
    NgxPaginationModule,
    BrowserModule,
    FormsModule,
    NgbModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatIconModule,
    HttpClientModule,
    MatGridListModule,
    MatCardModule,
    MatSelectModule,
    MatButtonModule,
    MatSnackBarModule,
    MatChipsModule,
    MatDialogModule
    ],
  providers: [{provide:HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true},
    {provide:HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true}],
  bootstrap: [AppComponent]
})

export class AppModule { }
