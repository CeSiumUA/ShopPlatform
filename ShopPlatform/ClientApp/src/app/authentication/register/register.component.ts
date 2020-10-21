import {Component, OnInit, Pipe, PipeTransform} from '@angular/core';
import {AuthenticationService} from '../authentication.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register',
  template: `
  <div class="container">
    <div class="py-5 text-center">
      <h2 i18n="ProfileImage|Label for ImageLabel">Choose profile image</h2>
    </div>
    <div class="row">
      <div class="col-md-4 order-md-2 mb-4">

      </div>
      <div class="col-md-8 order-md-1">
        <h4 class="mb-3" i18n="account information|Account information text">Account information</h4>
        <div class="needs-validation">
          <div class="row">
            <div class="col-md-6 mb-3">
              <label for="firstName" i18n="FirstName|Label for FirstName">First name</label>
              <input type="text" class="form-control" id="firstName" placeholder value required [(ngModel)]="account.FirstName">
              <div class="invalid-feedback" i18n="Invalid FirstName|Label invalid firstName">Valid first name is required</div>
            </div>
            <div class="col-md-6 mb-3">
              <label for="lastName" i18n="LastName|Label for LastName">Last name</label>
              <input type="text" class="form-control" id="lastName" placeholder value required [(ngModel)]="account.LastName">
              <div class="invalid-feedback" i18n="Invalid LastName|Label invalid lastName">Valid last name is required</div>
            </div>
          </div>
          <div class="mb-3">
            <label for="email" i18n="Email|Label for Email">Email</label>
            <input type="email" class="form-control" id="email" name="email" placeholder="account@example.com" [(ngModel)]="account.Email">
            <div class="invalid-feedback" i18n="Invalid Email|Label invalid email">Valid email is required</div>
          </div>
          <div class="mb-3">
            <label for="accountType" i18n="AccountType|Type of an account">Account Type</label>
            <select class="custom-select d-block w-100" id="accountType" [(ngModel)]="account.AccountType">
              <option *ngFor="let item of accountTypes | enumToArray" [value]="item.index" [label]="item.name"></option>
            </select>
          </div>
          <div class="mb-3">
            <label for="password" i18n="Password|Label for Password">Password</label>
            <input type="password" class="form-control" id="password" placeholder value required [(ngModel)]="account.PasswordStr">
            <div class="invalid-feedback" i18n="Invalid LastName|Label invalid lastName">Valid last name is required</div>
          </div>
          <button class="btn btn-primary btn-lg btn-block" i18n="Register Submit|Text for registration submit button" type="submit" (click)="registerAccount()">Register account</button>
        </div>
      </div>
    </div>
  </div>`
})
export class RegisterComponent implements OnInit{
  public account: ShopAccount;
  accountTypes = AccountType;
  constructor(private authService: AuthenticationService, private router: Router) {

  }
  public registerAccount(): void{
    this.authService.register(this.account);
  }
  ngOnInit(): void {
    this.account = new ShopAccount();
  }
}
export class ShopAccount{
  public VendorName: string;
  public AccountType: AccountType = AccountType.Customer;
  public PhotoUrl: string;
  public Email: string;
  public FirstName: string;
  public LastName: string;
  public PasswordStr: string;
}
export enum AccountType{
  Merchant = 0 ,
  Employee = 1,
  Customer= 2
}

@Pipe({name: 'enumToArray'})
export class EnumToArrayPipe implements PipeTransform {
  transform(value): Object {
    return Object.keys(value).filter(e => !isNaN(+e)).map(o => ({index: +o, name: value[o]}));
  }
}
