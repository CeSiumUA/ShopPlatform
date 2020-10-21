import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {LoginComponent} from './authentication/login/login.component';
import {RouterModule, Routes} from '@angular/router';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import {httpInterceptorsProviders} from './http/interceptors';
import {EnumToArrayPipe, RegisterComponent} from './authentication/register/register.component';
import {ShopComponent} from './shopmanagement/shop.component';
import {AuthGuard} from './authentication/auth.guard';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {NewshopComponent} from './shopmanagement/newshop.component';
import {HomeComponent} from './home.component';
import {NewitemComponent} from './shopmanagement/newitem.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'myshop', component: ShopComponent, canActivate: [AuthGuard]},
  {path: 'newshop', component: NewshopComponent, canActivate: [AuthGuard]}
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
    NewitemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    FormsModule,
    NgbModule
  ],
  providers: [
    HttpClient,
    httpInterceptorsProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
