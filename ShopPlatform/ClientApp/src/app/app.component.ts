import {Component, Inject, Injector, LOCALE_ID} from '@angular/core';
import {AuthenticationService} from './authentication/authentication.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-root',
  template: `
  <div class="d-flex flex-column flex-md-row align-items-center p-3 px-md-4 mb-3 bg-white border-bottom shadow-sm">
    <a class="navbar-brand mr-auto  my-0 mr-md-auto font-weight-normal" style="color: black" routerLink="/">Shop Platform</a>
    <nav class="my-2 my-md-0 mr-md-3">
    </nav>
    <button i18n="login button|This is a login button" class="btn btn-primary" routerLink="/login">Login</button>
  </div>
  <main role="main" class="container">
    <router-outlet></router-outlet>
  </main>`,
  styles: []
})
export class AppComponent {
  title = 'ShopPlatform';
  languageList = [
    {code: 'en', label: 'English'},
    {doce: 'he', label: 'Hebrew'}
  ];
  constructor(@Inject(LOCALE_ID) protected localeId: string, public authService: AuthenticationService, public routerService: Router) {
  }
}
