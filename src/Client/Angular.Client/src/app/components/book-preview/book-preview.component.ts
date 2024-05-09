import {Component, Input} from '@angular/core';
import {BookResponseDto} from "../../models/bookResponseDto";
import {apiUrl} from "../../app.config";

@Component({
  selector: 'app-book-preview',
  standalone: true,
  imports: [],
  templateUrl: './book-preview.component.html',
  styleUrl: './book-preview.component.css'
})
export class BookPreviewComponent {
  @Input() book!: BookResponseDto;
  protected readonly apiUrl = apiUrl;
}
