import { Component, OnInit } from '@angular/core';
import { Food } from 'src/app/models/food';
import { AccountService } from 'src/app/services/account.service';
import { FoodService } from 'src/app/services/food.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  loggedIn = false;
  newest: Food[] = [];
  topRated: Food[] = [];
  mostViewed: Food[] = [];
  ngOnInit(): void {
    this.getFoods();
    this.getCurrentUser();
  }
  constructor(private accountService: AccountService, private foodService: FoodService) { }
  getFoods() {
    this.foodService.getNewestFoods().subscribe({
      next: (response: Food[]) => {
        this.newest = response;
      }
    }
    );
    this.foodService.getMostViewedFoods().subscribe({
      next: (response: Food[]) => {
        this.mostViewed = response;
      }
    });
    this.foodService.getTopRatedFoods().subscribe({
      next: (response: Food[]) => {
        this.topRated = response;
      }
    });
  }
  getCurrentUser() {
    return this.accountService.currentUser$.subscribe({
      next: (response) => {
        this.loggedIn = !!response;
      },
    });
  }

}
