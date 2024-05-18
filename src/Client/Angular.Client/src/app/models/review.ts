import {User} from "./user";

export interface Review {
  id: string;
  bookId: number;
  userId: number;
  rating: number;
  text?: string | null;
  likeUserIds: number[];
  dislikeUserIds: number[];
  createAt: Date;
  user: User;
}
