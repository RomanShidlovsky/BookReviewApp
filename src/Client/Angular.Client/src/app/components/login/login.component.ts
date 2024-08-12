import {Component} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {Router, RouterLink} from "@angular/router";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import signIn from "../../models/signIn";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf,
    RouterLink
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  form: FormGroup;
  loginValid: boolean = true;
  message: string | undefined;
  constructor(
    private authService: AuthService,
    private router: Router,
    formBuilder: FormBuilder
  ) {
    this.form = formBuilder.group({
      userName: ['', [Validators.required]],
      password: ['', Validators.required],
    })
  }

  async login() {
    if (this.form.get('userName')?.errors) {
      this.message = "Username required."
      this.loginValid = false;

      return;
    }

    if (this.form.get('password')?.errors) {
      this.message = "Password required."
      this.loginValid = false;

      return;
    }

    let userCredentials: signIn = {
      userName: this.form.value.userName,
      password: this.form.value.password
    }

    let loggedIn = await this.authService.login({
      userName: this.form.value.userName,
      password: this.form.value.password
    });

    if (loggedIn) {
      await this.router.navigate(['/home']);
    } else {
      this.loginValid = false;
      this.message = 'Invalid user credentials.'
    }
  }
}
