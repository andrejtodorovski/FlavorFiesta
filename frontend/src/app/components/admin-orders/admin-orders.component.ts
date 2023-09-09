import { Component, OnInit } from '@angular/core';
import { OrderInfo } from 'src/app/models/orderInfo';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-admin-orders',
  templateUrl: './admin-orders.component.html',
  styleUrls: ['./admin-orders.component.css']
})
export class AdminOrdersComponent implements OnInit{
  orders: OrderInfo[] = []

  constructor(private orderService: OrderService) { }
  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders() {
    this.orderService.getAllOrders().subscribe({
      next: (response: OrderInfo[]) => {
        this.orders = response;
      }
    }); 
  }


}
