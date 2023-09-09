import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfo } from '../models/userInfo';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  userUrl = 'https://localhost:5001/api/User';

  constructor(private httpClient: HttpClient) { }

  getUserInfo(): Observable<UserInfo> {
    return this.httpClient.get<UserInfo>(this.userUrl + '/info');
  }
}
