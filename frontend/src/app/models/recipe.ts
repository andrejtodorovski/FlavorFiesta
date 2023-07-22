export interface Recipe {
    id: number;
    name: string;
    description: string;
    numberOfServings: number;
    difficulty: string;
    preparationTime: number;
    averageRating: number;
    viewCount: number;
    dateCreated: Date;
}