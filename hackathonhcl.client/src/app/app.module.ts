import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Route, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { LayoutComponent } from './layout/layout.component';
import { UserComponent } from './user/user.component';
import { AddUserComponent } from './add-user/add-user.component';
const routes: Route[] = [
  { path: 'login', component: LoginComponent },                   
  { path: 'app-root', component: AppComponent },                  
  { path: 'app-users', component: UserComponent },                   
  { path: 'app-add-user', component: AddUserComponent },                   // Wildcard route
  { path: '', pathMatch: 'full', redirectTo: 'login' },// Wildcard route
];


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LayoutComponent,
    UserComponent,
    AddUserComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, FormsModule, ReactiveFormsModule, RouterModule.forRoot(routes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
