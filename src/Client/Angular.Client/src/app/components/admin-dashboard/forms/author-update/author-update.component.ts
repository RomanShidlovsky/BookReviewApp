import {Component, OnInit} from '@angular/core';
import {FormGroup, ReactiveFormsModule} from "@angular/forms";
import {AuthorsService} from "../../../../api/authors.service";
import {ActivatedRoute, Router} from "@angular/router";
import {updateAuthorDtoForm} from "../../../../forms/bookApi";
import {UpdateAuthorDto} from "../../../../models/author/updateAuthorDto";
import {AuthorResponseDto} from "../../../../models/author/authorResponseDto";


@Component({
  selector: 'app-author-update',
  standalone: true,
    imports: [
        ReactiveFormsModule
    ],
  templateUrl: './author-update.component.html',
  styleUrl: './author-update.component.css'
})
export class AuthorUpdateComponent implements OnInit {
  author!: AuthorResponseDto;
  form: FormGroup;
  message: string | undefined;
  formValid: boolean = true;
  id: number;

  constructor(
    private activatedRoute: ActivatedRoute,
    private authorsService: AuthorsService,
    private router: Router
  ) {
    this.form = updateAuthorDtoForm;
    this.id = activatedRoute.snapshot.params["id"];
  }

  ngOnInit(): void {
    this.authorsService.apiAuthorsIdGet(this.id).subscribe({
      next: author => {
        this.author = author;
        this.form.setValue(author);
      },
      error: err => {
        console.log(err);
      }
    })
  }

  async update(files: FileList | null) {
    if (this.form.get('firstName')?.errors) {
      this.message = "FirstName required";
      this.formValid = false;

      return;
    }

    if (this.form.get('lastName')?.errors) {
      this.message = "LastName required";
      this.formValid = false;

      return;
    }

    if (this.form.get('fullName')?.errors) {
      this.message = "fullName required";
      this.formValid = false;

      return;
    }

    if (this.form.get('birthDate')?.errors) {
      this.message = "BirthDate required";
      this.formValid = false;

      return;
    }

    const updateAuthorDto: UpdateAuthorDto = {
      id: this.id,
      openLibraryKey: '',
      firstName: this.form.value.firstName,
      lastName: this.form.value.lastName,
      fullName: this.form.value.fullName,
      biography: this.form.value.biography,
      birthDate: this.form.value.birthDate,
      deathDate: this.form.value.deathDate
    }

    this.authorsService.apiAuthorsIdPut(this.id, updateAuthorDto).subscribe({
      next: async (value) => {
        await this.uploadFile(value.id, files)

        this.formValid = true;
        this.message = 'Updated';

        await this.router.navigate(['/admin-dashboard']);
      },
      error: err => {
        this.message = err.error.message;
        this.formValid = false;

        return;
      }
    });
  }

  async uploadFile(id: number, files: FileList | null) {
    if (!files) {
      return null;
    }

    if (files.length === 0) {
      return null;
    }

    let fileToUpload = <File>files[0];

    return this.authorsService.uploadFile(id, fileToUpload).subscribe({
      error: err => {
        console.log(err)
      }
    });
  }
}
