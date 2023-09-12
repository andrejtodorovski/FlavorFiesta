import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable, map } from 'rxjs';
import { AccountService } from '../services/account.service';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class AdminGuard {
  constructor(private accountService: AccountService) {}
  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map((user) => {
        if (user) {
          if (user.userName === 'admin') {
            return true;
          }
          return false;
        } else {
          return false;
        }
      })
    );
  }
}
