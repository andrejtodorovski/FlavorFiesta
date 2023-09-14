import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { OrderDTO } from 'src/app/models/orderDTO';
import { ShoppingCartInfo } from 'src/app/models/shoppingCartInfo';
import { ShoppingCartService } from 'src/app/services/shopping-cart.service';
import { StripeService } from 'src/app/services/stripe.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css'],
})
export class ShoppingCartComponent implements AfterViewInit, OnInit {
  @ViewChild('cardElement', { static: false }) cardElementRef!: ElementRef;
  card: any;

  shoppingCart: ShoppingCartInfo | undefined;
  orderInfo : OrderDTO = {
    address: '',
    phoneNumber: '',
    stripeToken: ''
  };

  constructor(
    private service: ShoppingCartService,
    private toastrService: ToastrService,
    private stripeService: StripeService
  ) {}
  ngAfterViewInit(): void {
    this.card = this.stripeService.getCardElement();
    this.card.on('ready', () => {
      this.cardElementRef.nativeElement.classList.add('StripeElement');
    });
  
    // Handle focus
    this.card.on('focus', () => {
      this.cardElementRef.nativeElement.classList.add('StripeElement--focus');
    });
    this.card.on('blur', () => {
      this.cardElementRef.nativeElement.classList.remove('StripeElement--focus');
    });
  
    // Handle validation errors
    this.card.on('change', (event: { error: any; }) => {
      if (event.error) {
        this.cardElementRef.nativeElement.classList.add('StripeElement--invalid');
      } else {
        this.cardElementRef.nativeElement.classList.remove('StripeElement--invalid');
      }
    });
    this.card.mount(this.cardElementRef.nativeElement);
  }

  ngOnInit(): void {
    this.getShoppingCartInfo();
  }
  
  async submitOrder() {
    const result = await this.stripeService.createToken(this.card);
    if (result.error) {
      this.toastrService.error(result.error.message);
      return;
    }
    else {
    this.orderInfo.stripeToken = result.token.id;
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
  }

  getShoppingCartInfo() {
    this.service.getShoppingCartInfo().subscribe((data) => {
      this.shoppingCart = data;
    });
  }
  async handlePayment() {
    const result = await this.stripeService.createToken(this.card);
    if (result.error) {
      console.error(result.error.message);
    } else {
      console.log(result.token);
      // Send the token to your backend for processing
    }
  }
}
