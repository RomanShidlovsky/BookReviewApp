import {Component} from '@angular/core';
import {FormGroup, ReactiveFormsModule} from "@angular/forms";
import {LanguagesService} from "../../../../api/languages.service";
import {Router} from "@angular/router";
import {createSubjectDtoForm} from "../../../../forms/bookApi";
import {CreateSubjectDto} from "../../../../models/subject/createSubjectDto";
import {SubjectsService} from "../../../../api/subjects.service";

@Component({
  selector: 'app-subject-create',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './subject-create.component.html',
  styleUrl: './subject-create.component.css'
})
export class SubjectCreateComponent {
  form: FormGroup;
  message: string | undefined;
  formValid: boolean = true;

  constructor(
    private subjectsService: SubjectsService,
    private router: Router,
  ) {
    this.form = createSubjectDtoForm;
  }

  async create() {
    if (this.form.get('name')?.errors) {
      this.message = "Name min length is 2 symbols.";
      this.formValid = false;

      return;
    }

    const CreateSubjectDto: CreateSubjectDto = {
      name: this.form.value.name
    }

    this.subjectsService.apiSubjectsPost(CreateSubjectDto).subscribe({
      error: err => {
        this.message = err.message;
        this.formValid = false;

        return;
      }
    });

    this.formValid = true;
    this.message = 'Created';

    await this.router.navigate(['/admin-dashboard']);
  }
}
