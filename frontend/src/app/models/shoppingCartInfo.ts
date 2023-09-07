import { CartItemInfo } from "./cartItemInfo";

export interface ShoppingCartInfo {
  id: number;
  status: string;
  totalPrice: number;
  appUserId: number;
  appUserName: string;
  cartItems: CartItemInfo[];
}
