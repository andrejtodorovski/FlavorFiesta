import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OrderInfo } from '../models/orderInfo';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  orderUrl = 'https://localhost:5001/api/order';
  constructor(private httpClient: HttpClient) { }
  
  getUserOrders() : Observable<OrderInfo[]> {
    return this.httpClient.get<OrderInfo[]>(this.orderUrl);
  }
}
