import {Component, OnInit} from '@angular/core';
import {FormGroup, ReactiveFormsModule} from "@angular/forms";
import {LanguagesService} from "../../../../api/languages.service";
import {ActivatedRoute, Router} from "@angular/router";
import {updateLanguageDtoForm} from "../../../../forms/bookApi";
import {CreateLanguageDto} from "../../../../models/language/createLanguageDto";
import {LanguageResponseDto} from "../../../../models/language/languageResponseDto";
import {UpdateLanguageDto} from "../../../../models/language/updateLanguageDto";

@Component({
  selector: 'app-language-update',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './language-update.component.html',
  styleUrl: './language-update.component.css'
})
export class LanguageUpdateComponent implements OnInit {
  language!: LanguageResponseDto;
  form: FormGroup;
  message: string | undefined;
  formValid: boolean = true;
  id: number;

  constructor(
    private activatedRoute: ActivatedRoute,
    private languagesService: LanguagesService,
    private router: Router,
  ) {
    this.form = updateLanguageDtoForm;
    this.id = activatedRoute.snapshot.params["id"];
  }

  ngOnInit() {
    this.languagesService.apiLanguagesIdGet(this.id).subscribe({
      next: language => {
        this.language = language;
        this.form.setValue(language);
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

    const updateLanguageDto: UpdateLanguageDto = {
      id: this.id,
      name: this.form.value.name
    }

    this.languagesService.apiLanguagesIdPut(this.id, updateLanguageDto).subscribe({
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
