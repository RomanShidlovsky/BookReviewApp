import {Component, input, OnInit} from '@angular/core';
import {BookPreviewComponent} from "../book-preview/book-preview.component";
import {NgForOf} from "@angular/common";
import {ActivatedRoute} from "@angular/router";
import {BookWithReviews} from "../../models/book";
import {BookService} from "../../services/book.service";
import {ReviewComponent} from "../review/review.component";
import {ReviewSectionComponent} from "../review-section/review-section.component";

@Component({
  selector: 'app-book',
  standalone: true,
  imports: [
    BookPreviewComponent,
    NgForOf,
    ReviewComponent,
    ReviewSectionComponent
  ],
  templateUrl: './book.component.html',
  styleUrl: './book.component.css'
})
export class BookComponent implements OnInit{
  id: number;
  book!: BookWithReviews;

  constructor(private activateRoute: ActivatedRoute, private bookService: BookService) {
    this.id = activateRoute.snapshot.params["id"];
  }

  ngOnInit(): void {
    this.bookService.getBookById(this.id)
      .subscribe({
        next: value => {
          this.book = value;
        },
        error: err => {
          console.log(err)
        }
      });
  }
}
