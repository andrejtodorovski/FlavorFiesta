import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Food } from '../models/food';
import { Observable } from 'rxjs';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class FoodService {

  foodUrl = 'https://localhost:5001/api/foods';
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

}
