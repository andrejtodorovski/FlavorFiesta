import { Food } from "./food";

export interface CartItemInfo {
    id: number;
    quantity: number;
    subtotal: number;
    food: Food
}