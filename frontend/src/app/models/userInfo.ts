import { FoodStats } from "./foodStats";
import { OrderInfo } from "./orderInfo";
import { Review } from "./review";
import { ReviewInfo } from "./reviewInfo";

export interface UserInfo {
    id: number;
    userName: string;
    photoUrl: string;
    reviews: ReviewInfo[];
    numberOfReviews: number;
    orders: OrderInfo[];
    numberOfOrders: number;
    mostOrderedFoods: FoodStats[];
}