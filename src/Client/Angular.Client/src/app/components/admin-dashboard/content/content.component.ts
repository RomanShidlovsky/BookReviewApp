import {Component, OnInit} from '@angular/core';
import {SectionService} from "../../../services/section.service";
import {NgSwitch, NgSwitchCase, NgSwitchDefault, UpperCasePipe} from "@angular/common";
import {BooksComponent} from "../books/books.component";
import {AuthorsComponent} from "../authors/authors.component";
import {LanguagesComponent} from "../languages/languages.component";
import {SubjectsComponent} from "../subjects/subjects.component";

@Component({
  selector: 'app-content',
  standalone: true,
  imports: [
    UpperCasePipe,
    NgSwitch,
    BooksComponent,
    NgSwitchCase,
    AuthorsComponent,
    LanguagesComponent,
    SubjectsComponent,
    NgSwitchDefault
  ],
  templateUrl: './content.component.html',
  styleUrl: './content.component.css'
})
export class ContentComponent implements OnInit {
  section: string = 'books';

  constructor(private sectionService: SectionService) {}
  ngOnInit(): void {
    this.sectionService.currentSection.subscribe(section => {
      this.section = section;
    })
  }
}
