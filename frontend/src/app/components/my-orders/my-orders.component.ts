import { Component, OnInit } from '@angular/core';
import { OrderInfo } from 'src/app/models/orderInfo';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css'],
})
export class MyOrdersComponent implements OnInit {
  orders: OrderInfo[] = [];

  constructor(private orderService: OrderService) {}
  ngOnInit(): void {
    this.loadUserOrders();
  }

  loadUserOrders() {
    this.orderService
      .getUserOrders()
      .subscribe((response) => (this.orders = response));
  }
  downloadInvoice(orderId: number) {
    this.orderService.downloadInvoice(orderId).subscribe({
      next: (response) => {
        const url = window.URL.createObjectURL(response);
        window.open(url);
      },
    });
  } 
}
