import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from '../authentication.service';
import {catchError} from 'rxjs/operators';
import {of} from 'rxjs';
import {Router} from '@angular/router';

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
        <img class="mb-4" src="https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSN3wAcYgtK2nz6SBIK0EZUfs9Tv8nv9V5l8g&usqp=CAU" alt width="72" height="72">
        <h1 i18n="login greetings|This is a login greeting" class="h3 mb-3 font-weight-normal">Sign In</h1>
        <label for="inputEmail" i18n="email placeholder|email address placeholder for login label" class="sr-only">Email address</label>
        <input type="email" id="inputEmail" class="form-control" [(ngModel)]="email" placeholder="Email" required autofocus>
        <label for="inputPassword" i18n="password placeholder|password placeholder for login label" class="sr-only">Password</label>
        <input type="password" id="inputPassword" class="form-control" [(ngModel)]="password" placeholder="Password" required>
        <p *ngIf="loginError" i18n="invalid login|Text when invalid login attempt" style="color: red">Invalid login attempt</p>
        <button class="btn btn-lg btn-primary btn-block" i18n="Sign In button|Label for sign in button" (click)="login()" type="submit">Sign In</button>
    </div>
  </div>`,
})
export class LoginComponent implements OnInit{
  public email: string;
  public password: string;
  public loginError: boolean;
  constructor(private authService: AuthenticationService, private router: Router) {
  }
  public login(): void{
    this.authService.authenticate(this.email, this.password)
      .pipe(catchError(value => of(this.showError(value))))
      .subscribe((data: any) => {
        if (data.error == null) {
          this.authService.LoggedUser = data.payload;
          this.router.navigateByUrl('/');
        }
        else{
          this.showError(data);
        }
      });
  }
  private showError(value: any): void{
    switch (value.error.errorCode){
      case 3:
        this.loginError = true;
    }
  }
  ngOnInit(): void {
  }
}
