export interface CreateReviewDto {
    bookId: number;
    userId: number;
    rating: number;
    text: string;
}
