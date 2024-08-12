import {Component} from '@angular/core';
import {FormGroup, ReactiveFormsModule} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {updateLanguageDtoForm} from "../../../../forms/bookApi";
import {SubjectsService} from "../../../../api/subjects.service";
import {SubjectResponseDto} from "../../../../models/subject/subjectResponseDto";
import {UpdateSubjectDto} from "../../../../models/subject/updateSubjectDto";

@Component({
  selector: 'app-subject-update',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './subject-update.component.html',
  styleUrl: './subject-update.component.css'
})
export class SubjectUpdateComponent {
  subject!: SubjectResponseDto;
  form: FormGroup;
  message: string | undefined;
  formValid: boolean = true;
  id: number;

  constructor(
    private activatedRoute: ActivatedRoute,
    private subjectsService: SubjectsService,
    private router: Router,
  ) {
    this.form = updateLanguageDtoForm;
    this.id = activatedRoute.snapshot.params["id"];
  }

  ngOnInit() {
    this.subjectsService.apiSubjectsIdGet(this.id).subscribe({
      next: subject => {
        this.subject = subject;
        this.form.setValue(subject);
      },
      error: err => {
        console.log(err);
      }
    })
  }

  async update() {
    if (this.form.get('name')?.errors) {
      this.message = "Name min length is 2 symbols.";
      this.formValid = false;

      return;
    }

    const updateSubjectDto: UpdateSubjectDto = {
      id: this.id,
      name: this.form.value.name
    }

    this.subjectsService.apiSubjectsIdPut(this.id, updateSubjectDto).subscribe({
      next: async () => {
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
}
