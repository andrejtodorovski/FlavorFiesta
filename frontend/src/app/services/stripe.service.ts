// stripe.service.ts
import { Injectable } from '@angular/core';
import { loadStripe } from '@stripe/stripe-js';

@Injectable({
  providedIn: 'root',
})
export class StripeService {
  private stripe: any;
  private elements: any;

  constructor() {
    this.initializeStripe();
  }

  async initializeStripe() {
    this.stripe = await loadStripe('pk_test_51NSLz2K20ihsRQhmdMTz4Vmbb7wcXpBl8Wx8D2zI4g86lPxWGjazCN3jH9HB6yOBDPiZyhhvDDN1fzs7Fcq2WOjz00GAATPQLZ');
    this.elements = this.stripe.elements();
  }

  getCardElement() {
    return this.elements.create('card');
  }

  async createToken(card: any) {
    return await this.stripe.createToken(card);
  }
}
