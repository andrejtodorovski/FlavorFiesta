export interface ReviewInfo {
    id: number;
    rating: number;
    comment: string;
    dateReviewed: Date;
    foodName: string;
    foodPhotoUrl: string;
    foodAverageRating: number;
    appUserName: string;
    appUserPhotoUrl: string;
}