import {Component, OnInit} from '@angular/core';
import {BookPreviewComponent} from "../book-preview/book-preview.component";
import {NgForOf} from "@angular/common";
import {ActivatedRoute} from "@angular/router";
import {ReviewComponent} from "../review/review.component";
import {ReviewSectionComponent} from "../review-section/review-section.component";
import {CriticReviewSectionComponent} from "../critic-review-section/critic-review-section.component";
import {BookWithReviewsResponseDto} from "../../models/book/bookResponseDto";
import {BooksService} from "../../api/books.service";

@Component({
  selector: 'app-book',
  standalone: true,
  imports: [
    BookPreviewComponent,
    NgForOf,
    ReviewComponent,
    ReviewSectionComponent,
    CriticReviewSectionComponent
  ],
  templateUrl: './book.component.html',
  styleUrl: './book.component.css'
})
export class BookComponent implements OnInit {
  id: number;
  book!: BookWithReviewsResponseDto;

  constructor(private activateRoute: ActivatedRoute, private _booksService: BooksService) {
    this.id = activateRoute.snapshot.params["id"];
  }

  ngOnInit(): void {
    this._booksService.apiBooksIdGet(this.id)
      .subscribe({
        next: value => {
          this.book = value;
        },
        error: err => {
          console.log(err)
        }
      });
  }

  getAuthorsNames() {
    return this.book.authors.map(a => a.fullName).join(', ');
  }
}
