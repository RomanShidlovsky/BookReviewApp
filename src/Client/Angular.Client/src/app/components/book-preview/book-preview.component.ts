import {Component, Input} from '@angular/core';
import {Book} from "../../models/book";

@Component({
  selector: 'app-book-preview',
  standalone: true,
  imports: [],
  templateUrl: './book-preview.component.html',
  styleUrl: './book-preview.component.css'
})
export class BookPreviewComponent {
  @Input() book!: Book;
}
