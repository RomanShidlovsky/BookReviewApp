import { Comment } from './comment';

export interface AddCommentToReviewDto {
    reviewId: string;
    comment: Comment;
}
