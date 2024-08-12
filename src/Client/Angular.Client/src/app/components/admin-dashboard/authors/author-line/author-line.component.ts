import {Component, EventEmitter, Input, Output} from '@angular/core';
import {BooksService} from "../../../../api/books.service";
import {AuthorResponseDto} from "../../../../models/author/authorResponseDto";
import {RouterLink} from "@angular/router";
import {AuthorsService} from "../../../../api/authors.service";

@Component({
  selector: 'app-author-line',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './author-line.component.html',
  styleUrl: './author-line.component.css'
})
export class AuthorLineComponent {
  @Input() author!: AuthorResponseDto;
  @Output() onAuthorDeleted = new EventEmitter<number>();

  constructor(private authorsService: AuthorsService) {
  }

  confirmDeletion() {
    const confirmed = confirm('Are you sure that you want to delete this item?');

    if (confirmed) {
      this.deleteAuthor();
    }
  }

  deleteAuthor() {
    this.authorsService.apiAuthorsIdDelete(this.author.id).subscribe({
      next: deleted => {
        if (deleted === true) {
          this.onAuthorDeleted.emit(this.author.id);
        }
      }
    });
  }
}
