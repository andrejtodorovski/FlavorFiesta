import { Component, OnInit } from '@angular/core';
import { Recipe } from 'src/app/models/recipe';
import { AccountService } from 'src/app/services/account.service';
import { RecipeService } from 'src/app/services/recipe.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  loggedIn = false;
  newest: Recipe[] = [];
  topRated: Recipe[] = [];
  mostViewed: Recipe[] = [];
  ngOnInit(): void {
    this.getRecipes();
    this.getCurrentUser();
  }
  constructor(private accountService: AccountService, private recipeService: RecipeService) { }
  getRecipes() {
    this.recipeService.getNewestRecipes().subscribe({
      next: (response: Recipe[]) => {
        this.newest = response;
      }
    }
    );
    this.recipeService.getMostViewedRecipes().subscribe({
      next: (response: Recipe[]) => {
        this.mostViewed = response;
      }
    });
    this.recipeService.getTopRatedRecipes().subscribe({
      next: (response: Recipe[]) => {
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
