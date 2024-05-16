import {Component, OnInit} from '@angular/core';
import {Book} from "../../models/book";
import {BookService} from "../../services/book.service";
import {NgForOf} from "@angular/common";
import {BookPreviewComponent} from "../book-preview/book-preview.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    NgForOf,
    BookPreviewComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  lastUpdatedBooks: Book[] = [];

  constructor(private _bookService: BookService) {
  }

  ngOnInit() {
    this._bookService.getLastUpdatedBooks(3)
      .subscribe(books => this.lastUpdatedBooks = books);
  }
}
