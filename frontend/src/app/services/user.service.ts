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

  getUserReviews(): Observable<any> {
    return this.httpClient.get<any>(this.userUrl + '/reviews');
  }

  getAllUsers(): Observable<UserInfo[]> {
    return this.httpClient.get<UserInfo[]>(this.userUrl + '/all');
  }

  importUsers(formData: FormData): Observable<any> {
    return this.httpClient.post(this.userUrl + '/import', formData);
  }

  sendEmail(contact: any): Observable<any> {
    var emailInfo = {
      mailTo: contact.email,
      subject: contact.subject,
      content: contact.message
    }
    return this.httpClient.post(this.userUrl + '/contact', emailInfo);
  }
}
