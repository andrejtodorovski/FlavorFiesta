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

  getAllOrders() : Observable<OrderInfo[]> {
    return this.httpClient.get<OrderInfo[]>(this.orderUrl + '/all');
  }

  setOrderStatusToFinished(orderId: number) : Observable<void> {
    return this.httpClient.put<void>(this.orderUrl + '/update-status-to-finished/' + orderId, null);
  }
  downloadInvoice(orderId: number) : Observable<Blob> {
    return this.httpClient.get(`${this.orderUrl}/download-invoice/${orderId}`, {
      responseType: 'blob'
    });
  }
  downloadOrdersReport() : Observable<Blob> {
    return this.httpClient.get(`${this.orderUrl}/orders-report`, {
      responseType: 'blob'
    });
  }
}
