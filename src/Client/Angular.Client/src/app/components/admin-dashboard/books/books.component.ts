import {Component, OnInit} from '@angular/core';
import {BooksService} from "../../../api/books.service";
import {BookResponseDto} from "../../../models/book/bookResponseDto";
import {NgForOf} from "@angular/common";
import {BookLineComponent} from "./book-line/book-line.component";

@Component({
  selector: 'app-books',
  standalone: true,
  imports: [
    NgForOf,
    BookLineComponent
  ],
  templateUrl: './books.component.html',
  styleUrl: './books.component.css'
})
export class BooksComponent implements OnInit {
  books!: BookResponseDto[];

  constructor(private booksService: BooksService) {
  }

  ngOnInit(): void {
    this.booksService.apiBooksGet().subscribe({
      next: value => {
        this.books = value;
      },
      error: err => {
        console.log(err)
      }
    });
  }

  onBookDeleted(id: number) {
    this.books = this.books.filter(book => book.id != id);
  }
}
