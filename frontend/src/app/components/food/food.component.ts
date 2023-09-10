import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AddToCart } from 'src/app/models/addToCart';
import { Food } from 'src/app/models/food';
import { Review } from 'src/app/models/review';
import { ReviewDTO } from 'src/app/models/reviewDTO';
import { FoodService } from 'src/app/services/food.service';

@Component({
  selector: 'app-food',
  templateUrl: './food.component.html',
  styleUrls: ['./food.component.css']
})
export class FoodComponent implements OnInit {
  food: Food | null = null
  id: any;
  isFoodAddedToUserShoppingCart: boolean = false;
  isFoodReviewedByUser: boolean = false;
  review: ReviewDTO = {
    rating: 0,
    comment: ''
  }
  addToCart: AddToCart = {
    foodId: 0,
    quantity: 1
  }
  constructor(private foodService: FoodService, private route: ActivatedRoute, private router: Router, private toastrService: ToastrService) {}
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.id = params.get('id');
      if(this.id!=null){
        this.getFoodById(parseInt(this.id));
        this.checkIfFoodIsInUserShoppingCart(parseInt(this.id));
        this.checkIfFoodIsReviewedByUser(parseInt(this.id));
      }
    });
  }
  getFoodById(id: number) {
    this.foodService.getFoodById(id).subscribe({
      next: (response: Food) => {
        this.food = response;
        this.addToCart.foodId = this.food.id;
      },
      error: (error) => {
        if(error)
          this.router.navigateByUrl('/not-found');
      }
    }); 
  }

  checkIfFoodIsReviewedByUser(id: number) {
    this.foodService.isFoodReviewedByUser(id).subscribe({
      next: (response: boolean) => {
        this.isFoodReviewedByUser = response;
      }
    });
  }

  checkIfFoodIsInUserShoppingCart(id: number) {
    this.foodService.isFoodInUserShoppingCart(id).subscribe({
      next: (response: boolean) => {
        this.isFoodAddedToUserShoppingCart = response;
      }
    });
  }


  addFoodToCart() {
    this.foodService.addFoodToCard(this.addToCart).subscribe({
      next: () => {
        this.toastrService.success('Food added to cart successfully');
        this.isFoodAddedToUserShoppingCart = true;
      }
    });
  }

  leaveReview() {
    this.foodService.leaveReview(this.review, this.food!.id).subscribe({
      next: () => {
        this.toastrService.success('Review added successfully');
        this.isFoodReviewedByUser = true;
        this.getFoodById(this.food!.id);
      }
    });
  }

  isStarFilledRating(rating: number, starNumber: number): boolean {
    return starNumber <= rating;
  }

  isStarFilled(averageRating: number, starNumber: number): boolean {
    return starNumber <= Math.floor(averageRating) ||
    (starNumber > Math.floor(averageRating) && averageRating > starNumber - 0.25 && averageRating <= starNumber + 0.25);
  }
  isStarHalfFilled(averageRating: number, starNumber: number): boolean {
    return starNumber > Math.floor(averageRating) && starNumber <= Math.ceil(averageRating)
    && averageRating > starNumber-0.75 && averageRating <= starNumber-0.25;
  }

}
