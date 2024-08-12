import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {UsersService} from "../../api/users.service";
import {ActivatedRoute, Router} from "@angular/router";
import {NgIf} from "@angular/common";
import {UpdateUserDto} from "../../models/user/updateUserDto";

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit {
  profileForm: FormGroup;
  userId: number;
  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private usersService: UsersService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
    this.profileForm = this.fb.group({
      userName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });

    this.userId = activatedRoute.snapshot.params["id"];
  }

  ngOnInit(): void {
    this.usersService.apiUsersIdGet(this.userId).subscribe((user) => {
      this.profileForm.patchValue(user);
    });
  }

  onSubmit(): void {
    if (this.profileForm.valid) {
      const updateUserDto: UpdateUserDto = {
        id: this.userId,
        userName: this.profileForm.value.userName,
        email: this.profileForm.value.email,
      }

      this.usersService.apiUsersIdPut(this.userId, updateUserDto).subscribe({
        next: value => {
          this.router.navigate(['/home']);
          this.message = null;
        },
        error: response => {
          this.message = response.error.message;
          console.log(response);
        }
      });
    }
  }
}
