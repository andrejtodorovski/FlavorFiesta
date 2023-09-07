import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { OrderInfo } from 'src/app/models/orderInfo';
import { ShoppingCartInfo } from 'src/app/models/shoppingCartInfo';
import { ShoppingCartService } from 'src/app/services/shopping-cart.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css'],
})
export class ShoppingCartComponent implements OnInit {
  shoppingCart: ShoppingCartInfo | undefined;
  orderInfo : OrderInfo = {
    address: '',
    phoneNumber: '',
  };

  constructor(
    private service: ShoppingCartService,
    private toastrService: ToastrService,
  ) {}

  ngOnInit(): void {
    this.service.getShoppingCartInfo().subscribe((data) => {
      this.shoppingCart = data;
    });
  }
  
  submitOrder(): void {
    this.service.placeOrder(this.orderInfo).subscribe({
      next: () => {
        this.toastrService.success('Order placed successfully!');
      },
      error: (err) => {
        this.toastrService.error(err.error);
      },
    });
  }
}
