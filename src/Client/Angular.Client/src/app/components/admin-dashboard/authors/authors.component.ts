import {Component, EventEmitter, Input, Output} from '@angular/core';
import {BookResponseDto} from "../../../models/book/bookResponseDto";
import {BooksService} from "../../../api/books.service";
import {AuthorResponseDto} from "../../../models/author/authorResponseDto";
import {AuthorsService} from "../../../api/authors.service";
import {BookLineComponent} from "../books/book-line/book-line.component";
import {NgForOf} from "@angular/common";
import {AuthorLineComponent} from "./author-line/author-line.component";

@Component({
  selector: 'app-authors',
  standalone: true,
  imports: [
    BookLineComponent,
    NgForOf,
    AuthorLineComponent
  ],
  templateUrl: './authors.component.html',
  styleUrl: './authors.component.css'
})
export class AuthorsComponent {
  authors!: AuthorResponseDto[]
  constructor(private authorsService: AuthorsService) {
  }

  ngOnInit(): void {
    this.authorsService.apiAuthorsGet().subscribe({
      next: value => {
        this.authors = value;
      },
      error: err => {
        console.log(err)
      }
    });
  }

  onAuthorDeleted(id: number) {
    this.authors = this.authors.filter(author => author.id != id);
  }
}
