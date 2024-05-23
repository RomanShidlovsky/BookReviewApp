import {Component, OnInit} from '@angular/core';

import {NgForOf} from "@angular/common";
import {BookPreviewComponent} from "../book-preview/book-preview.component";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {BookResponseDto} from "../../models/book/bookResponseDto";
import {AuthorsService} from "../../api/authors.service";
import {BooksService} from "../../api/books.service";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    NgForOf,
    BookPreviewComponent,
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  lastUpdatedBooks: BookResponseDto[] = [];

  constructor(private _booksService: BooksService) {
  }

  ngOnInit() {
    this._booksService.apiBooksGet('', '', 1, 3)
      .subscribe({
        next: value => {
          this.lastUpdatedBooks = value;
        },
        error: err => {
          console.log(err)
        }
      });
  }
}
