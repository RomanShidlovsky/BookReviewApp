import {Component, EventEmitter, Input, Output} from '@angular/core';
import {AuthorsService} from "../../../../api/authors.service";
import {LanguageResponseDto} from "../../../../models/language/languageResponseDto";
import {RouterLink} from "@angular/router";
import {LanguagesService} from "../../../../api/languages.service";
import {BooksService} from "../../../../api/books.service";

@Component({
  selector: 'app-language-line',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './language-line.component.html',
  styleUrl: './language-line.component.css'
})
export class LanguageLineComponent {
  @Input() language!: LanguageResponseDto;
  @Output() onLanguageDeleted = new EventEmitter<number>();

  constructor(private languagesService: LanguagesService) {
  }

  confirmDeletion() {
    const confirmed = confirm('Are you sure that you want to delete this item?');

    if (confirmed) {
      this.deleteLanguage();
    }
  }

  deleteLanguage() {
    this.languagesService.apiLanguagesIdDelete(this.language.id).subscribe({
      next: deleted => {
        if (deleted === true) {
          this.onLanguageDeleted.emit(this.language.id);
        }
      }
    });
  }
}
