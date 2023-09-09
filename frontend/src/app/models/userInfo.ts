import { FoodStats } from "./foodStats";
import { OrderInfo } from "./orderInfo";
import { Review } from "./review";

export interface UserInfo {
    id: number;
    userName: string;
    photoUrl: string;
    reviews: Review[];
    numberOfReviews: number;
    orders: OrderInfo[];
    numberOfOrders: number;
    mostOrderedFoods: FoodStats[];
}