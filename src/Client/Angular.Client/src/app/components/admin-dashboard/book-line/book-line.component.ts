import {Component, EventEmitter, Input, Output} from '@angular/core';
import {BookResponseDto} from "../../../models/book/bookResponseDto";
import {RouterLink} from "@angular/router";
import {BooksService} from "../../../api/books.service";


@Component({
  selector: 'app-book-line',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './book-line.component.html',
  styleUrl: './book-line.component.css'
})
export class BookLineComponent {
  @Input() book!: BookResponseDto;
  @Output() onBookDeleted = new EventEmitter<number>();
  protected readonly alert = alert;
  protected readonly console = console;

  constructor(private booksService: BooksService) {
  }

  confirmDeletion() {
    const confirmed = confirm('Are you sure that you want to delete this item?');

    if (confirmed) {
      this.deleteBook();
    }
  }

  deleteBook() {
    this.booksService.apiBooksIdDelete(this.book.id).subscribe({
      next: deleted => {
        if (deleted === true) {
          this.onBookDeleted.emit(this.book.id);
        }
      }
    });
  }
}
