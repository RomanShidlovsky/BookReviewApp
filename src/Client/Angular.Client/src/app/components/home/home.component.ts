import {Component, OnInit} from '@angular/core';
import {Book} from "../../models/book";
import {BookService} from "../../services/book.service";
import {NgForOf} from "@angular/common";
import {BookPreviewComponent} from "../book-preview/book-preview.component";
import {RouterLink, RouterLinkActive} from "@angular/router";

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
  lastUpdatedBooks: Book[] = [];

  constructor(private _bookService: BookService) {
  }

  ngOnInit() {
    this._bookService.getLastUpdatedBooks(3)
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
