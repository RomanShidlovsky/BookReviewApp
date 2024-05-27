import {Component} from '@angular/core';
import {FormGroup, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {Router} from "@angular/router";
import {createAuthorDtoForm} from "../../../../forms/bookApi";
import {AuthorsService} from "../../../../api/authors.service";
import {CreateAuthorDto} from "../../../../models/author/createAuthorDto";
import {MatFormField} from "@angular/material/form-field";
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from "@angular/material/datepicker";
import {MatInput} from "@angular/material/input";

@Component({
  selector: 'app-author-create',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    FormsModule,
    MatFormField,
    MatDatepickerInput,
    MatDatepickerToggle,
    MatDatepicker,
    MatInput
  ],
  templateUrl: './author-create.component.html',
  styleUrl: './author-create.component.css'
})
export class AuthorCreateComponent {
  form: FormGroup;
  message: string | undefined;
  formValid: boolean = true;


  constructor(
    private authorsService: AuthorsService,
    private router: Router,
  ) {
    this.form = createAuthorDtoForm;
  }

  async create() {
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

    const createAuthorDto: CreateAuthorDto = {
      firstName: this.form.value.firstName,
      lastName: this.form.value.lastName,
      fullName: this.form.value.fullName,
      biography: this.form.value.biography,
      birthDate: this.form.value.birthDate,
      deathDate: this.form.value.deathDate
    }

    this.authorsService.apiAuthorsPost(createAuthorDto).subscribe({
      next: async () => {
        this.formValid = true;
        this.message = 'Created';

        await this.router.navigate(['/admin-dashboard']);
      },
      error: err => {
        this.message = err.message;
        this.formValid = false;

        return;
      }
    });
  }
}
