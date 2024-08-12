import { Component, OnInit, OnDestroy } from '@angular/core';
import { BookPreviewComponent } from "../book-preview/book-preview.component";
import { NgForOf } from "@angular/common";
import { ActivatedRoute, RouterLink } from "@angular/router";
import { ReviewComponent } from "../review/review.component";
import { ReviewSectionComponent } from "../review-section/review-section.component";
import { CriticReviewSectionComponent } from "../critic-review-section/critic-review-section.component";
import { BookResponseDto, BookWithReviewsResponseDto } from "../../models/book/bookResponseDto";
import { BooksService } from "../../api/books.service";
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-book',
  standalone: true,
  imports: [
    BookPreviewComponent,
    NgForOf,
    ReviewComponent,
    ReviewSectionComponent,
    CriticReviewSectionComponent,
    RouterLink
  ],
  templateUrl: './book.component.html',
  styleUrl: './book.component.css'
})
export class BookComponent implements OnInit, OnDestroy {
  id!: number;
  book!: BookWithReviewsResponseDto;
  recommendedBooks: BookResponseDto[] = [];
  private routeSub!: Subscription;

  constructor(private activateRoute: ActivatedRoute, private _booksService: BooksService) {}

  ngOnInit(): void {
    this.routeSub = this.activateRoute.params.subscribe(params => {
      this.id = params['id'];
      this.loadBook();
      this.loadRecommendedBooks();
    });
  }

  ngOnDestroy(): void {
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }
  }

  loadBook(): void {
    this._booksService.apiBooksIdGet(this.id).subscribe({
      next: value => {
        this.book = value;
      },
      error: err => {
        console.error(err);
      }
    });
  }

  loadRecommendedBooks(): void {
    this._booksService.apiBooksRecommendedGet(this.id, 3).subscribe({
      next: books => {
        this.recommendedBooks = books;
      },
      error: err => {
        console.error(err);
      }
    });
  }

  getAuthorsNames(): string {
    return this.book.authors.map(a => a.fullName).join(', ');
  }
}
