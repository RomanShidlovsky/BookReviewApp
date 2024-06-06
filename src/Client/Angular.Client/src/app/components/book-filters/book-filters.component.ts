import {AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule} from "@angular/forms";
import {SubjectsService} from "../../api/subjects.service";
import {LanguagesService} from "../../api/languages.service";
import {AuthorsService} from "../../api/authors.service";
import {BooksService} from "../../api/books.service";
import {IDropdownSettings, NgMultiSelectDropDownModule} from "ng-multiselect-dropdown";
import {LanguageResponseDto} from "../../models/language/languageResponseDto";
import {SubjectResponseDto} from "../../models/subject/subjectResponseDto";
import {AuthorResponseDto} from "../../models/author/authorResponseDto";
import {BookResponseDto} from "../../models/book/bookResponseDto";
import {NgForOf, NgIf} from "@angular/common";
import {BookViewComponent} from "./book-view/book-view.component";

interface Item {
  id: number;
}

@Component({
  selector: 'app-book-filters',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgMultiSelectDropDownModule,
    NgForOf,
    NgIf,
    BookViewComponent
  ],
  templateUrl: './book-filters.component.html',
  styleUrl: './book-filters.component.css'
})
export class BookFiltersComponent implements OnInit, AfterViewInit {
  filterForm: FormGroup;
  languages: LanguageResponseDto[] = [];
  subjects: SubjectResponseDto[] = [];
  authors: AuthorResponseDto[] = [];
  books: BookResponseDto[] = [];
  dropdownWithNameSettings: IDropdownSettings = {};
  dropdownWithFullNameSettings: IDropdownSettings = {};
  pageSize = 3;
  currentPage = 1;
  isLoading = false;
  isLoaded = false;
  filterQueryString = '';
  orderByQueryString = '';

  @ViewChild('scrollContainer', { static: false }) scrollContainer!: ElementRef;

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
    };

    this.dropdownWithFullNameSettings = {
      idField: 'id',
      textField: 'fullName'
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
    });

    this.authorsService.apiAuthorsGet().subscribe({
      next: authors => {
        this.authors = authors;
      },
      error: err => {
        console.log(err);
      }
    });

    this.loadMoreBooks();
  }

  ngAfterViewInit(): void {
    this.scrollContainer.nativeElement.addEventListener('scroll', () => this.onScroll());
  }

  loadMoreBooks() {
    if (this.isLoading || this.isLoaded) {
      return;
    }

    this.isLoading = true;

    const { selectedLanguages, selectedSubjects, selectedAuthors }: { selectedLanguages: Item[], selectedSubjects: Item[], selectedAuthors: Item[] } = this.filterForm.value;

    console.log("lang", selectedLanguages);
    console.log("subj", selectedSubjects);
    console.log("lang", selectedAuthors);


    const selectedLanguagesIds = selectedLanguages.map((language: Item) => language.id);
    const selectedSubjectsIds = selectedSubjects.map((subject: Item) => subject.id);
    const selectedAuthorsIds = selectedAuthors.map((author: Item) => author.id);

    this.booksService.apiBooksGet(
      this.filterQueryString,
      this.orderByQueryString,
      this.currentPage,
      this.pageSize,
      selectedSubjectsIds,
      selectedLanguagesIds,
      selectedAuthorsIds
    ).subscribe({
      next: books => {
        if (books.length != 0) {
          /*const { selectedLanguages, selectedSubjects, selectedAuthors }: { selectedLanguages: Item[], selectedSubjects: Item[], selectedAuthors: Item[] } = this.filterForm.value;

          const selectedLanguagesIds = selectedLanguages.map((language: Item) => language.id);
          const selectedSubjectsIds = selectedSubjects.map((subject: Item) => subject.id);
          const selectedAuthorsIds = selectedAuthors.map((author: Item) => author.id);

          console.log(selectedSubjectsIds);

          const filteredBooks = books.filter(book => {
            const hasSelectedLanguages = selectedLanguagesIds.length === 0 || book.languages.some(language => selectedLanguagesIds.includes(language.id));
            const hasSelectedSubjects = selectedSubjectsIds.length === 0 || book.subjects.some(subject => selectedSubjectsIds.includes(subject.id));
            console.log(hasSelectedSubjects);
            const hasSelectedAuthors = selectedAuthorsIds.length === 0 || book.authors.some(author => selectedAuthorsIds.includes(author.id));
            return hasSelectedLanguages && hasSelectedSubjects && hasSelectedAuthors;
          });*/



          this.books = [...this.books, ...books];

          this.currentPage++;
        } else {
          this.isLoaded = true;
        }

        this.isLoading = false;
      },
      error: err => {
        console.log(err);
      }
    });
  }

  onScroll(): void {
    const { scrollTop, scrollHeight, clientHeight } = this.scrollContainer.nativeElement;
    if (scrollTop + clientHeight >= scrollHeight - 1) {
      this.loadMoreBooks();
    }
  }

  getQueryString(): string {
    const formValues = this.filterForm.value;

    return Object.entries(formValues)
      .filter(([key, value]) => value && !Array.isArray(value))
      .map(([key, value]) => `${key}=${value}`)
      .join(',');
  }

  onSubmit() {
    this.filterQueryString = this.getQueryString();
    console.log(this.filterQueryString);
    this.currentPage = 1;
    this.books = [];
    this.isLoaded = false;
    this.isLoading = false;
    this.loadMoreBooks();
  }

  clearFilters() {
    this.filterForm.reset({
      selectedLanguages: [],
      selectedSubjects: [],
      selectedAuthors: []
    });
  }
}
