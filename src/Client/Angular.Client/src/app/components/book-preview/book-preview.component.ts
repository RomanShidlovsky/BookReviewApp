import {Component, Input} from '@angular/core';
import {NgClass} from "@angular/common";
import {BookResponseDto} from "../../models/book/bookResponseDto";

@Component({
  selector: 'app-book-preview',
  standalone: true,
  imports: [
    NgClass
  ],
  templateUrl: './book-preview.component.html',
  styleUrl: './book-preview.component.css'
})
export class BookPreviewComponent {
  @Input() book!: BookResponseDto;
}
