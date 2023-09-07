import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ShoppingCartInfo } from '../models/shoppingCartInfo';
import { Observable } from 'rxjs';
import { OrderInfo } from '../models/orderInfo';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {

  shoppingCartUrl = 'https://localhost:5001/api/ShoppingCart';
  constructor(private httpClient: HttpClient) { }

  getShoppingCartInfo() : Observable<ShoppingCartInfo> {
    return this.httpClient.get<ShoppingCartInfo>(this.shoppingCartUrl);
  }

  placeOrder(orderInfo: OrderInfo) {
    return this.httpClient.post(this.shoppingCartUrl, orderInfo);
  }
  
}
