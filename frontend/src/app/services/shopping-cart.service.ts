import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ShoppingCartInfo } from '../models/shoppingCartInfo';
import { Observable } from 'rxjs';
import { OrderDTO } from '../models/orderDTO';
// import { OrderStripe } from '../models/orderStripe';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {

  shoppingCartUrl = 'https://localhost:5001/api/ShoppingCart';
  constructor(private httpClient: HttpClient) { }

  getShoppingCartInfo() : Observable<ShoppingCartInfo> {
    return this.httpClient.get<ShoppingCartInfo>(this.shoppingCartUrl);
  }

  placeOrder(orderInfo: OrderDTO) {
    return this.httpClient.post(this.shoppingCartUrl, orderInfo);
  }

  // createOrder(order: OrderStripe): Observable<string> {       
  //   return this.httpClient.post<string>(`${this.shoppingCartUrl}/payment/PlaceOrder`, order)    
  //  }
  
}
