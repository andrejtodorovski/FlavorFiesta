import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map, switchMap } from 'rxjs';
import { Food } from 'src/app/models/food';
import { FoodService } from 'src/app/services/food.service';

@Component({
  selector: 'app-foods',
  templateUrl: './foods.component.html',
  styleUrls: ['./foods.component.css'],
})
export class FoodsComponent implements OnInit {
  foods: Food[] = [];
  categoryId: number = 0;

  constructor(
    private foodService: FoodService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadFoods();
  }

  loadFoods() {
    this.route.queryParams.pipe(
      map((params) => +params['category'] || 0),
      switchMap((categoryId) =>
        categoryId === 0
          ? this.foodService.getFoods()
          : this.foodService.getFoodsByCategory(categoryId)
      )
    ).subscribe((foods) => {
      this.foods = foods;
    });
  }
}
