import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  loggedIn = false;
  ngOnInit(): void {
    this.getCurrentUser();
  }
  constructor(private accountService: AccountService) {}
  logout() {
    this.accountService.logout();
  }
  getCurrentUser() {
    return this.accountService.currentUser$.subscribe({
      next: (response) => {
        this.loggedIn = !!response;
      },
    });
  }
}
