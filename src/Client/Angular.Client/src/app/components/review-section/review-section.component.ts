import {Component, Input} from '@angular/core';
import {Review} from "../../models/review";
import {Book, BookWithReviews} from "../../models/book";
import {NgClass, NgForOf} from "@angular/common";
import {ReviewComponent} from "../review/review.component";

@Component({
  selector: 'app-review-section',
  standalone: true,
  imports: [
    NgClass,
    ReviewComponent,
    NgForOf
  ],
  templateUrl: './review-section.component.html',
  styleUrl: './review-section.component.css'
})
export class ReviewSectionComponent {
  @Input() title!: string;
  @Input() book!: BookWithReviews;
  @Input() reviews: Review[] = [];
}
