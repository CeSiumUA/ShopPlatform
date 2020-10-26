import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {LoginComponent} from './authentication/login/login.component';
import {RouterModule, Routes} from '@angular/router';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {httpInterceptorsProviders} from './http/interceptors';
import {EnumToArrayPipe, RegisterComponent} from './authentication/register/register.component';
import {ShopComponent} from './shopmanagement/shop.component';
import {AuthGuard} from './authentication/auth.guard';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {NewshopComponent} from './shopmanagement/newshop.component';
import {HomeComponent} from './home.component';
import {NewitemComponent} from './shopmanagement/newitem.component';
import {ShopselectorComponent} from "./shopmanagement/shopselector.component";
import {AuthenticationService} from "./authentication/authentication.service";
import {ToastscontainerComponent} from "./utils/toast/toastscontainer.component";
import {ItemviewComponent} from "./shopmanagement/itemview.component";

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'items/:id', component: ItemviewComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'myshop', component: ShopComponent, canActivate: [AuthGuard]},
  {path: 'newshop', component: NewshopComponent, canActivate: [AuthGuard]},
  {path: 'shops/:id', component: ShopselectorComponent, canActivate: [AuthGuard]},
  {path: 'newitem', component: NewitemComponent, canActivate: [AuthGuard]}
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    EnumToArrayPipe,
    ShopComponent,
    NewshopComponent,
    HomeComponent,
    NewitemComponent,
    ShopselectorComponent,
    ToastscontainerComponent,
    ItemviewComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    FormsModule,
    NgbModule,
    ReactiveFormsModule
  ],
  providers: [
    HttpClient,
    httpInterceptorsProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
