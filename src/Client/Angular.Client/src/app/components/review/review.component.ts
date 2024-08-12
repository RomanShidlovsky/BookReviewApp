import {Component, Input, OnInit} from '@angular/core';
import {Review} from "../../models/review";
import {NgClass, NgForOf} from "@angular/common";
import {AuthService} from "../../services/auth.service";
import {ReviewService} from "../../services/review.service";
import {CriticReviewService} from "../../services/critic-review.service";

@Component({
  selector: 'app-review',
  standalone: true,
  imports: [
    NgForOf,
    NgClass
  ],
  templateUrl: './review.component.html',
  styleUrl: './review.component.css'
})
export class ReviewComponent implements OnInit{
  @Input() review!: Review;
  @Input() isCritic!: boolean;
  liked: boolean = false;
  disliked: boolean = false;

  constructor(
    private _authService: AuthService,
    private _reviewService: ReviewService,
    private _criticReviewService: CriticReviewService) {}

  ngOnInit(): void {
    const userId = this._authService.getUserId();

    if (userId) {
      this.liked = this.review.likeUserIds.includes(userId);
      this.disliked = this.review.dislikeUserIds.includes(userId);
    }
  }

  async like() {
    const userId = this._authService.getUserId();

    if (!userId) {
      return;
    }

    const reviewService = this.isCritic
      ? this._criticReviewService
      : this._reviewService;

    if (this.liked) {
      const result = await reviewService.unlike(this.review.id, userId);

      result.subscribe({
        next: () => {
          this.liked = false;
          this.review.likeUserIds = this.review.likeUserIds.filter(num => num != userId);
        },
        error: err => {
          console.log(err)
        }
      });
    } else {
      const result = await reviewService.like(this.review.id, userId);

      result.subscribe({
        next: async () => {
          this.liked = true;
          this.review.likeUserIds.push(userId);

          if (this.disliked) {
            const dislikeResult = await reviewService.undislike(this.review.id, userId);

            dislikeResult.subscribe({
              next: () => {
                this.disliked = false;
                this.review.dislikeUserIds = this.review.dislikeUserIds.filter(num => num != userId);
              },
              error: err => {
                console.log(err)
              }
            })
          }
        },
        error: err => {
          console.log(err)
        }
      });
    }
  }

  async dislike() {
    const userId = this._authService.getUserId();

    if (!userId) {
      return;
    }

    const reviewService = this.isCritic
      ? this._criticReviewService
      : this._reviewService;

    if (this.disliked) {
      const result = await reviewService.undislike(this.review.id, userId);

      result.subscribe({
        next: () => {
          this.disliked = false;
          this.review.dislikeUserIds = this.review.dislikeUserIds.filter(num => num != userId);
        },
        error: err => {
          console.log(err)
        }
      });
    } else {
      const result = await reviewService.dislike(this.review.id, userId);

      result.subscribe({
        next: async () => {
          this.disliked = true;
          this.review.dislikeUserIds.push(userId);

          if (this.liked) {
            const likedResult = await reviewService.unlike(this.review.id, userId);

            likedResult.subscribe({
              next: () => {
                this.liked = false;
                this.review.likeUserIds = this.review.likeUserIds.filter(num => num != userId);
              },
              error: err => {
                console.log(err)
              }
            })
          }
        },
        error: err => {
          console.log(err)
        }
      });
    }
  }

  isLogged() {
    return this._authService.isLogged();
  }
}
