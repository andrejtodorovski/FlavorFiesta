import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { OrderDTO } from 'src/app/models/orderDTO';
import { ShoppingCartInfo } from 'src/app/models/shoppingCartInfo';
import { ShoppingCartService } from 'src/app/services/shopping-cart.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css'],
})
export class ShoppingCartComponent implements OnInit {
  shoppingCart: ShoppingCartInfo | undefined;
  orderInfo : OrderDTO = {
    address: '',
    phoneNumber: '',
  };

  constructor(
    private service: ShoppingCartService,
    private toastrService: ToastrService,
  ) {}

  ngOnInit(): void {
    this.getShoppingCartInfo();
  }
  
  submitOrder(): void {
    this.service.placeOrder(this.orderInfo).subscribe({
      next: () => {
        this.toastrService.success('Order placed successfully!');
        this.getShoppingCartInfo();
        this.orderInfo.address = '';
        this.orderInfo.phoneNumber = '';
      },
      error: (err) => {
        this.toastrService.error(err.error);
      },
    });
  }

  getShoppingCartInfo() {
    this.service.getShoppingCartInfo().subscribe((data) => {
      this.shoppingCart = data;
    });
  }
}
