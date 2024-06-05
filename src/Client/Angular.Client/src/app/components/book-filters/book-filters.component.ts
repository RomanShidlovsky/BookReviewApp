import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule} from "@angular/forms";
import {SubjectsService} from "../../api/subjects.service";
import {LanguagesService} from "../../api/languages.service";
import {AuthorsService} from "../../api/authors.service";
import {BooksService} from "../../api/books.service";
import {IDropdownSettings, NgMultiSelectDropDownModule} from "ng-multiselect-dropdown";
import {LanguageResponseDto} from "../../models/language/languageResponseDto";
import {SubjectResponseDto} from "../../models/subject/subjectResponseDto";

@Component({
  selector: 'app-book-filters',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgMultiSelectDropDownModule
  ],
  templateUrl: './book-filters.component.html',
  styleUrl: './book-filters.component.css'
})
export class BookFiltersComponent implements OnInit {
  filterForm: FormGroup;
  languages: LanguageResponseDto[] = [];
  subjects: SubjectResponseDto[] = [];
  dropdownWithNameSettings: IDropdownSettings = {};

  constructor(
    private fb: FormBuilder,
    private subjectsService: SubjectsService,
    private languagesService: LanguagesService,
    private authorsService: AuthorsService,
    private booksService: BooksService
  ) {
    this.filterForm = this.fb.group({
      title: [''],
      publicationYearStart: [''],
      publicationYearEnd: [''],
      selectedSubjects: [[], []],
      selectedAuthors: [[], []],
      selectedLanguages: [[], []]
    });

    this.dropdownWithNameSettings = {
      idField: 'id',
      textField: 'name'
    }
  }

  ngOnInit() {
    this.subjectsService.apiSubjectsGet().subscribe({
      next: subjects => {
        this.subjects = subjects;
      },
      error: err => {
        console.log(err);
      }
    });

    this.languagesService.apiLanguagesGet().subscribe({
      next: languages => {
        this.languages = languages;
      },
      error: err => {
        console.log(err);
      }
    })
  }

  onSubmit() {
    console.log('Filters applied:', this.filterForm.value);
  }

  clearFilters() {
    this.filterForm.reset();
  }
}
