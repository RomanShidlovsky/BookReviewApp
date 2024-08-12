import { BookResponseDto } from './bookResponseDto';
import { Comment } from './comment';
import { UserResponseDto } from './userResponseDto';

export interface ReviewResponseDto {
    id: string;
    bookId: number;
    userId: number;
    rating: number;
    text: string;
    likeUserIds: Array<number>;
    dislikeUserIds: Array<number>;
    comments: Array<Comment>;
    user: UserResponseDto;
    book: BookResponseDto;
}
