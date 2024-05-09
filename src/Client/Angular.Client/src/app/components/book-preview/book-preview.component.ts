import {Component, Input} from '@angular/core';
import {Book} from "../../models/book";
import {apiUrl} from "../../app.config";

@Component({
  selector: 'app-book-preview',
  standalone: true,
  imports: [],
  templateUrl: './book-preview.component.html',
  styleUrl: './book-preview.component.css'
})
export class BookPreviewComponent {
  @Input() book!: Book;
  protected readonly apiUrl = apiUrl;
}
