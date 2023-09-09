import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginDTO } from '../models/loginDTO';
import { BehaviorSubject, map } from 'rxjs';
import { RegisterDTO } from '../models/registerDTO';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  constructor(private httpClient: HttpClient) {}

  login(loginDTO: LoginDTO) {
    return this.httpClient
      .post<User>(this.baseUrl + 'account/login', loginDTO)
      .pipe(
        map((response: User) => {
          const user = response;
          if (user) {            
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSource.next(user);
          }
        })
      );
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
  register(registerDTO: RegisterDTO) {
    return this.httpClient
      .post<User>(this.baseUrl + 'account/register', registerDTO)
      .pipe(
        map((response: User) => {
          if (response) {
            localStorage.setItem('user', JSON.stringify(response));
            this.currentUserSource.next(response);
          }
        })
      );
  }
}
