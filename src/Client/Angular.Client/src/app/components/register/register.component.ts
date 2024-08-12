import { Component } from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import {Router, RouterLink} from "@angular/router";
import {BookPreviewComponent} from "../book-preview/book-preview.component";
import {NgForOf, NgIf} from "@angular/common";
import signUp from "../../models/signUp";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    FormsModule,
    BookPreviewComponent,
    NgForOf,
    RouterLink,
    NgIf
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  form: FormGroup;
  registerValid: boolean = true;
  message: string | undefined;

  constructor(
      private authService: AuthService,
      private router: Router,
      formBuilder: FormBuilder
  ) {
    this.form = formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      userName: ['', [Validators.required]],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    })
  }

  async register() {
    if (this.form.get('userName')?.errors) {
      this.message = "Username required."
      this.registerValid = false;

      return;
    }

    if (this.form.get('email')?.errors) {
      this.message = "Invalid email."
      this.registerValid = false;

      return
    }

    if (this.form.get('password')?.errors) {
      this.message = "Password required."
      this.registerValid = false;

      return;
    }

    if (this.form.get('confirmPassword')?.errors) {
      this.message = "Confirm password required."
      this.registerValid = false;

      return;
    }

    if (this.form.value.password !== this.form.value.confirmPassword) {
      this.message = "Invalid confirm password."
      this.registerValid = false;

      return;
    }

    let userCredentials: signUp = {
      userName: this.form.value.userName,
      email: this.form.value.email,
      password: this.form.value.password
    }

    let registred = (await this.authService.register(userCredentials))
      .subscribe( {
        next: async () => {
          let signedIn = await this.authService.login({
            userName: userCredentials.userName,
            password: userCredentials.password});


          if (signedIn) {
            await this.router.navigate(['/home']);
          } else {
            this.message = "Can't to sign in.";
            this.registerValid = false;

            return;
          }
        },
        error: response => {
          if (response.error.message) {
            this.message = response.error.message;
          } else {
            this.message = "Something went wrong."
          }

          this.registerValid = false;

          return
        }
      })
  }
}
