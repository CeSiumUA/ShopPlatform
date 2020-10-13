import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService{
  public LoggedUser: any;
  constructor(private httpClient: HttpClient) {
    console.log('Auth service started!');
  }
  public authenticate(email: string, password: string): void{
    this.httpClient.post(`https://localhost:5001/api/authentication/login`, JSON.stringify({Email: `${email}`, Password: `${password}`})).subscribe();
  }
}
