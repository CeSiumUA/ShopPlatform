import {Component, Inject, Injector, LOCALE_ID, OnInit} from '@angular/core';
import {AuthenticationService} from './authentication/authentication.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-root',
  template: `
<!--  <div class="d-flex flex-column flex-md-row align-items-center p-3 px-md-4 mb-3 bg-white border-bottom shadow-sm">-->
<!--    <a class="navbar-brand mr-auto  my-0 mr-md-auto font-weight-normal" style="color: black" routerLink="/">Shop Platform</a>-->
    <nav class="navbar navbar-expand-lg navbar-light bg-white border-bottom shadow-sm">
      <a class="navbar-brand" style="color: black" routerLink="/">Shop Platform</a>
      <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav mr-auto">
        </ul>
        <div class="form-inline my-2 my-lg-0">
          <input class="form-control" type="search" placeholder="Search" aria-label="Search">
          <button class="btn btn-outline-success my-2 my-sm-0 mr-sm-2">Search</button>
          <div *ngIf="authService.LoggedUser == null" class="mr-sm-2">
            <button i18n="login button|This is a login button" class="btn btn-primary" routerLink="/login">Login</button>
            <button class="btn btn-secondary" i18n="register button|This is a register button" routerLink="/register">Register</button>
          </div>
          <div class="nav-item dropdown mr-sm-2" *ngIf="authService.LoggedUser != null">
            <div ngbDropdown class="d-inline-block">
              <button i18n="GreetingLabel|Greeting label for logged user" class="btn btn-outline-primary" id="dropdownBasic1" ngbDropdownToggle>Hello, {{authService.LoggedUser.firstName}}!</button>
              <div ngbDropdownMenu aria-labelledby="dropdownBasic1">
                <button ngbDropdownItem i18n="MyProfileLabel|Label for user profile link" routerLink="/{{authService.LoggedUser.profileId}}">My Profile</button>
                <button ngbDropdownItem i18n="MyShopLabel|Label for my shop button" routerLink="/myshop">My Shop</button>
                <button ngbDropdownItem i18n="LogoutLabel|Label for logout button" (click)="authService.logout()">Logout</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </nav>
<!--  </div>-->
  <main role="main" class="container">
    <router-outlet></router-outlet>
  </main>`,
  styles: []
})
export class AppComponent implements OnInit{
  title = 'ShopPlatform';
  languageList = [
    {code: 'en', label: 'English'},
    {code: 'he', label: 'Hebrew'}
  ];
  constructor(@Inject(LOCALE_ID) protected localeId: string, public authService: AuthenticationService, public routerService: Router) {
  }
  ngOnInit(): void {
  }
}
