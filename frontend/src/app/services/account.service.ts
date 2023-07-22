import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginDTO } from '../models/loginDTO';
import { BehaviorSubject, map } from 'rxjs';
import { RegisterDTO } from '../models/registerDTO';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';
  private currentUserSource = new BehaviorSubject<LoginDTO | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  constructor(private httpClient: HttpClient) {}

  login(loginDTO: LoginDTO) {
    return this.httpClient
      .post<LoginDTO>(this.baseUrl + 'account/login', loginDTO)
      .pipe(
        map((response: LoginDTO) => {
          const user = response;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSource.next(user);
          }
        })
      );
  }

  setCurrentUser(loginDTO: LoginDTO) {
    this.currentUserSource.next(loginDTO);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
  register(registerDTO: RegisterDTO) {
    return this.httpClient
      .post<RegisterDTO>(this.baseUrl + 'account/register', registerDTO)
      .pipe(
        map((response: RegisterDTO) => {
          if (response) {
            localStorage.setItem('user', JSON.stringify(response));
            this.currentUserSource.next(response);
          }
        })
      );
  }
}
