import { Injectable } from '@angular/core';

import { Observable, map, of } from 'rxjs';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard  {
  constructor(private acccountService: AccountService) {}
  canActivate(): Observable<boolean> {
    return this.acccountService.currentUser$.pipe(
      map((user) => {
        if (user) {
          return true;
        }
        return false;
      })
    );
  }
}
