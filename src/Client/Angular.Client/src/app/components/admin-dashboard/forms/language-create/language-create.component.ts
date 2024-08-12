import {Component} from '@angular/core';
import {createLanguageDtoForm} from "../../../../forms/bookApi";
import {FormGroup, ReactiveFormsModule} from "@angular/forms";
import {NgIf} from "@angular/common";
import {Router} from "@angular/router";
import {LanguagesService} from "../../../../api/languages.service";
import {CreateLanguageDto} from "../../../../models/language/createLanguageDto";

@Component({
  selector: 'app-language-create',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './language-create.component.html',
  styleUrl: './language-create.component.css'
})
export class LanguageCreateComponent {
  form: FormGroup;
  message: string | undefined;
  formValid: boolean = true;

  constructor(
    private languagesService: LanguagesService,
    private router: Router,
  ) {
    this.form = createLanguageDtoForm;
  }

  async create() {
    if (this.form.get('name')?.errors) {
      this.message = "Name min length is 2 symbols.";
      this.formValid = false;

      return;
    }

    const createLanguageDto: CreateLanguageDto = {
      name: this.form.value.name
    }

    this.languagesService.apiLanguagesPost(createLanguageDto).subscribe({
      next: async () => {
        this.formValid = true;
        this.message = 'Created';

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
