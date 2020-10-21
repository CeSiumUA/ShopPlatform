import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ShopAccount} from './register/register.component';
import {Router} from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService{
  get LoggedUser(): SavedUser{
    return JSON.parse(localStorage.getItem('user'));
  }
  set LoggedUser(value: SavedUser){
    localStorage.setItem('user', JSON.stringify(value));
  }
  constructor(private httpClient: HttpClient, private router: Router) {
  }
  public authenticate(email: string, password: string): Observable<any>{
    return this.httpClient.post(`/api/authentication/login`, JSON.stringify({Email: `${email}`, Password: `${password}`}));
  }
  public logout(): void{
    localStorage.removeItem('user');
    window.location.reload();
  }
  public register(user: any): void{
    this.httpClient.post('/api/authentication/register', JSON.stringify(user)).subscribe((data: any) => {
      if (data.error == null) {
        this.LoggedUser = data.payload;
        this.router.navigateByUrl('/');
      }
      else{
      }
    });
  }
}
export class SavedUser{
  public profileId: string;
  public accessToken: string;
  public refreshToken: string;
  public firstName: string;
  public lastName: string;
  public PhotoUrl: string;
}
