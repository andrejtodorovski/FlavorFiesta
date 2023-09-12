import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from 'src/app/models/category';
import { AccountService } from 'src/app/services/account.service';
import { FoodService } from 'src/app/services/food.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  loggedIn = false;
  isAdmin = false;
  categories : Category[] = [];
  ngOnInit(): void {
    this.getCurrentUser();
    this.getCategories();
  }
  constructor(private accountService: AccountService, private foodService: FoodService) {}
  logout() {
    this.accountService.logout();
  }
  getCurrentUser() {
    return this.accountService.currentUser$.subscribe({
      next: (response) => {
        this.isAdmin = response?.userName === 'admin';
        this.loggedIn = !!response;
      },
    });
  }

  getCategories() {
    this.foodService.getCategories().subscribe({
      next: (response) => {
        this.categories = response;
      },
    });
  }
}
