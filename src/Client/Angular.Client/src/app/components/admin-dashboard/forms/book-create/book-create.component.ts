import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {AuthorsService} from "../../../../api/authors.service";
import {Router} from "@angular/router";
import {createBookDtoForm} from "../../../../forms/bookApi";
import {LanguageResponseDto} from "../../../../models/language/languageResponseDto";
import {SubjectResponseDto} from "../../../../models/subject/subjectResponseDto";
import {AuthorResponseDto} from "../../../../models/author/authorResponseDto";
import {LanguagesService} from "../../../../api/languages.service";
import {SubjectsService} from "../../../../api/subjects.service";
import {BooksService} from "../../../../api/books.service";
import {NgForOf} from "@angular/common";
import {IDropdownSettings, NgMultiSelectDropDownModule} from "ng-multiselect-dropdown";
import {CreateBookDto} from "../../../../models/book/createBookDto";
import {AddLanguageToBookDto} from "../../../../models/language/addLanguageToBookDto";
import {B} from "@angular/cdk/keycodes";
import {AddSubjectToBookDto} from "../../../../models/subject/addSubjectToBookDto";
import {AddAuthorToBookDto} from "../../../../models/author/addAuthorToBookDto";

@Component({
  selector: 'app-book-create',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    FormsModule,
    NgForOf,
    NgMultiSelectDropDownModule
  ],
  templateUrl: './book-create.component.html',
  styleUrl: './book-create.component.css'
})
export class BookCreateComponent implements OnInit {
  form: FormGroup;
  message: string | undefined;
  formValid: boolean = true;
  languages: LanguageResponseDto[] = [];
  subjects: SubjectResponseDto[] = [];
  authors: AuthorResponseDto[] = [];
  dropdownWithNameSettings: IDropdownSettings = {};
  dropdownWithFullNameSettings: IDropdownSettings = {};

  constructor(
    private booksService: BooksService,
    private subjectsService: SubjectsService,
    private languagesService: LanguagesService,
    private authorsService: AuthorsService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.form = createBookDtoForm;

    this.dropdownWithNameSettings = {
      idField: 'id',
      textField: 'name'
    }

    this.dropdownWithFullNameSettings = {
      idField: 'id',
      textField: 'fullName'
    }
  }

  ngOnInit() {
    this.languagesService.apiLanguagesGet().subscribe({
      next: languages => {
        this.languages = languages;
      },
      error: err => {
        console.log(err);
      }
    });

    this.subjectsService.apiSubjectsGet().subscribe({
      next: subjects => {
        this.subjects = subjects;
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
    })
  }

  async create(files: FileList | null) {
    console.log(this.languages);
    console.log(this.form.value);

    if (this.form.get('title')?.errors) {
      this.message = "Title required";
      this.formValid = false;

      return;
    }

    if (this.form.get('editionCount')?.errors) {
      this.message = "Edition Count required";
      this.formValid = false;

      return;
    }

    if (this.form.get('publicationYear')?.errors) {
      this.message = "Publication Year required";
      this.formValid = false;

      return;
    }


    const createBookDto: CreateBookDto = {
      title: this.form.value.title,
      editionCount: this.form.value.editionCount,
      publicationYear: this.form.value.publicationYear,
      openLibraryKey: null
    }

    this.booksService.apiBooksPost(createBookDto).subscribe({
      next: async (book) => {
        await this.uploadFile(book.id, files);

        const selectedLanguages: { id: number }[] = this.form.value.selectedLanguages;
        const selectedSubjects: { id: number }[] = this.form.value.selectedSubjects;
        const selectedAuthors: { id: number }[] = this.form.value.selectedAuthors;

        selectedLanguages.forEach((language) => {
          const addLanguageToBookDto: AddLanguageToBookDto = {
            bookId: book.id,
            languageId: language.id
          };

          console.log(addLanguageToBookDto);

          this.booksService.apiBooksIdLanguagesPut(book.id, addLanguageToBookDto).subscribe({
            error: err => {
              console.log(err);
            }
          })
        });

        selectedSubjects.forEach((subject) => {
          const addSubjectToBookDto: AddSubjectToBookDto = {
            bookId: book.id,
            subjectId: subject.id
          };

          this.booksService.apiBooksIdSubjectsPut(book.id, addSubjectToBookDto).subscribe({
            error: err => {
              console.log(err);
            }
          })
        });

        selectedAuthors.forEach((author) => {
          const addAuthorToBookDto: AddAuthorToBookDto = {
            bookId: book.id,
            authorId: author.id
          };

          this.booksService.apiBooksIdAuthorsPut(book.id, addAuthorToBookDto).subscribe({
            error: err => {
              console.log(err);
            }
          })
        });

        this.formValid = true;
        this.message = 'Created';

        await this.router.navigate(['/admin-dashboard']);
      },
      error: err => {
        this.message = err.error.message;
        console.log(err);
      }
    })
  }

  async uploadFile(id: number, files: FileList | null) {
    if (!files) {
      return null;
    }

    if (files.length === 0) {
      return null;
    }

    let fileToUpload = <File>files[0];

    return this.booksService.uploadFile(id, fileToUpload).subscribe({
      error: err => {
        console.log(err)
      }
    });
  }
}
