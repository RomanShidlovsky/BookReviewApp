import {Component} from '@angular/core';
import {LanguageResponseDto} from "../../../models/language/languageResponseDto";
import {LanguagesService} from "../../../api/languages.service";
import {BookLineComponent} from "../books/book-line/book-line.component";
import {NgForOf} from "@angular/common";
import {LanguageLineComponent} from "./language-line/language-line.component";

@Component({
  selector: 'app-languages',
  standalone: true,
  imports: [
    BookLineComponent,
    NgForOf,
    LanguageLineComponent
  ],
  templateUrl: './languages.component.html',
  styleUrl: './languages.component.css'
})
export class LanguagesComponent {
  languages!: LanguageResponseDto[]
  constructor(private languagesService: LanguagesService) {
  }

  ngOnInit(): void {
    this.languagesService.apiLanguagesGet().subscribe({
      next: value => {
        this.languages = value;
      },
      error: err => {
        console.log(err)
      }
    });
  }

  onLanguageDeleted(id: number) {
    this.languages = this.languages.filter(language => language.id != id);
  }
}
