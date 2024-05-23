import {Component, Input} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import {CreateReviewModel} from "../../models/review";
import {ReviewComponent} from "../review/review.component";
import {NgClass, NgForOf} from "@angular/common";
import {CriticReviewService} from "../../services/critic-review.service";
import {BookWithReviewsResponseDto} from "../../models/book/bookResponseDto";

@Component({
  selector: 'app-critic-review-section',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    ReviewComponent,
    NgClass,
    NgForOf
  ],
  templateUrl: './critic-review-section.component.html',
  styleUrl: './critic-review-section.component.css'
})
export class CriticReviewSectionComponent {
  @Input() title!: string;
  @Input() book!: BookWithReviewsResponseDto;
  @Input() IsCritic!: boolean;

  form: FormGroup;
  message: string | undefined;
  success: boolean = false;

  constructor(
    formBuilder: FormBuilder,
    private _reviewService: CriticReviewService,
    private _authService: AuthService) {
    this.form = formBuilder.group({
      text: ['', [Validators.required]],
      rating: ['', [Validators.required]]
    })
  }

  async post() {
    if (!this._authService.isLogged()) {
      this.message = "Only authorized users can post reviews";
      this.success = false;

      return;
    }

    if (this.form.get('text')?.errors) {
      this.message = "Enter review text."
      this.success = false;

      return;
    }

    if (this.form.get('rating')?.errors) {
      this.message = "Set book rating."
      this.success = false;

      return;
    }

    const review: CreateReviewModel = {
      userId: this._authService.getUserId()!,
      bookId: this.book.id!,
      text: this.form.value.text,
      rating: this.form.value.rating
    }

    const posted = await this._reviewService.post(review);

    posted.subscribe({
      next: (value) => {
        this.message = 'Posted';
        this.book.criticReviews.push(value);
        this.book.averageCriticRating = this.calculateAverageRating();
        this.success = true;
      },
      error: (err) => {
        console.log(err);
        this.message = 'Something went wrong';
        this.success = false;
      }
    });
  }

  calculateAverageRating(): number {
    if (this.book.criticReviews.length === 0) {
      return 0;
    }

    const totalRating = this.book.criticReviews.reduce((sum, review) => sum + review.rating, 0);
    return totalRating / this.book.criticReviews.length;
  }
}
