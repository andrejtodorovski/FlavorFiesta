import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Food } from '../models/food';
import { Observable } from 'rxjs';
import { Category } from '../models/category';
import { AddToCart } from '../models/addToCart';
import { ReviewInfo } from '../models/reviewInfo';

@Injectable({
  providedIn: 'root'
})
export class FoodService {

  foodUrl = 'https://localhost:5001/api/food';
  constructor(private httpClient: HttpClient) { }
  getFoods(): Observable<Food[]> {
    return this.httpClient.get<Food[]>(this.foodUrl);
  }
  getNewestFoods(): Observable<Food[]> {
    return this.httpClient.get<Food[]>(`${this.foodUrl}/newest`);
  }
  getMostViewedFoods(): Observable<Food[]> {
    return this.httpClient.get<Food[]>(`${this.foodUrl}/most-viewed`);
  }
  getTopRatedFoods(): Observable<Food[]> {
    return this.httpClient.get<Food[]>(`${this.foodUrl}/top-rated`);
  }
  getFoodById(id: number): Observable<Food> {
    return this.httpClient.get<Food>(`${this.foodUrl}/${id}`);
  }
  getCategories(): Observable<Category[]> {
    return this.httpClient.get<Category[]>(`${this.foodUrl}/categories`);
  }
  getFoodsByCategory(categoryId: number): Observable<Food[]> {
    return this.httpClient.get<Food[]>(`${this.foodUrl}/category/${categoryId}`);
  }
  addFoodToCard(addToCart : AddToCart): Observable<void> {
    return this.httpClient.post<void>(`${this.foodUrl}/add-to-cart`, addToCart);
  }
  isFoodInUserShoppingCart(foodId: number): Observable<boolean> {
    return this.httpClient.get<boolean>(`${this.foodUrl}/is-food-in-user-shopping-cart/${foodId}`);
  }
  isFoodReviewedByUser(foodId: number): Observable<boolean> {
    return this.httpClient.get<boolean>(`${this.foodUrl}/is-food-reviewed-by-user/${foodId}`);
  }
  leaveReview(review: any, foodId: number): Observable<void> {
    return this.httpClient.post<void>(`${this.foodUrl}/leave-review/${foodId}`, review);
  }
  getReviewsForFood(foodId: number): Observable<ReviewInfo[]> {
    return this.httpClient.get<ReviewInfo[]>(`${this.foodUrl}/reviews/${foodId}`);
  }
  downloadMenu(): Observable<Blob> {
    return this.httpClient.get(`${this.foodUrl}/menu`, {
      responseType: 'blob'
    });
  } 
}
