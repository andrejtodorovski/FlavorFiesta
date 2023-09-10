import { Category } from "./category";
import { Ingredient } from "./ingredient";
import { Review } from "./review";

export interface Food {
    id: number;
    name: string;
    description: string;
    price: number;
    averageRating: number;
    viewCount: number;
    dateCreated: Date;
    calories: number;
    photoUrl: string;
    ingredients: Ingredient[];
    categories: Category[];
    reviews: Review[];
}