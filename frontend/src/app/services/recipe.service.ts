import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Recipe } from '../models/recipe';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  recipeUrl = 'https://localhost:5001/api/recipes';
  constructor(private httpClient: HttpClient) { }
  getRecipes(): Observable<Recipe[]> {
    return this.httpClient.get<Recipe[]>(this.recipeUrl);
  }
  getNewestRecipes(): Observable<Recipe[]> {
    return this.httpClient.get<Recipe[]>(`${this.recipeUrl}/newest`);
  }
  getMostViewedRecipes(): Observable<Recipe[]> {
    return this.httpClient.get<Recipe[]>(`${this.recipeUrl}/mostViewed`);
  }
  getTopRatedRecipes(): Observable<Recipe[]> {
    return this.httpClient.get<Recipe[]>(`${this.recipeUrl}/topRated`);
  }

}
