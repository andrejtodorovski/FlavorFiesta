import { ShoppingCartInfo } from "./shoppingCartInfo";

export interface OrderInfo {
    id: number,
    phoneNumber: string,
    address: string,
    orderStatus: string,
    appUserName: string,
    shoppingCart: ShoppingCartInfo
}