import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from '../authentication.service';

@Component({
  selector: 'app-login',
  styles: ['.form-signin {\n' +
  '    width: 100%;\n' +
  '    max-width: 330px;\n' +
  '    padding: 15px;\n' +
  '    margin: auto;\n' +
  '}', '.text-center {\n' +
  '    text-align: center!important;\n' +
  '}'],
  template: `
  <div class="text-center">
    <div class="form-signin">
        <img class="mb-4" src="https://getbootstrap.com/docs/4.5/assets/brand/bootstrap-solid.svg" alt width="72" height="72">
        <h1 i18n="login greetings|This is a login greeting" class="h3 mb-3 font-weight-normal">Sign In</h1>
        <label for="inputEmail" i18n="email placeholder|email address placeholder for login label" class="sr-only">Email address</label>
        <input type="email" id="inputEmail" class="form-control" [(ngModel)]="email" placeholder="Email" required autofocus>
        <label for="inputPassword" i18n="password placeholder|password placeholder for login label" class="sr-only">Password</label>
        <input type="password" id="inputPassword" class="form-control" [(ngModel)]="password" placeholder="Password" required>
        <button class="btn btn-lg btn-primary btn-block" i18n="Sign In button|Label for sign in button" (click)="login()" type="submit">Sign In</button>
    </div>
  </div>`,
})
export class LoginComponent implements OnInit{
  public email: string;
  public password: string;
  constructor(private authService: AuthenticationService) {
  }
  public login(): void{
    this.authService.authenticate(this.email, this.password);
  }
  ngOnInit(): void {
  }
}
