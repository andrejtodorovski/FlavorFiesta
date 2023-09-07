import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  orderUrl = 'https://localhost:5001/api/orders';
  constructor(private httpClient: HttpClient) { }
  
}
