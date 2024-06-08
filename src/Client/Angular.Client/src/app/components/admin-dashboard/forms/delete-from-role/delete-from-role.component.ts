import { Component } from '@angular/core';
import {FormGroup, ReactiveFormsModule} from "@angular/forms";
import {UserResponseDto} from "../../../../models/review/userResponseDto";
import {RoleDto} from "../../../../models/role/roleDto";
import {UsersService} from "../../../../api/users.service";
import {RolesService} from "../../../../api/roles.service";
import {Router} from "@angular/router";
import {AddUserToRoleDto} from "../../../../models/user/addUserToRoleDto";
import {NgForOf} from "@angular/common";
import {addToRole} from "../../../../forms/bookApi";

@Component({
  selector: 'app-delete-from-role',
  standalone: true,
  imports: [
    NgForOf,
    ReactiveFormsModule
  ],
  templateUrl: './delete-from-role.component.html',
  styleUrl: './delete-from-role.component.css'
})
export class DeleteFromRoleComponent {
  form: FormGroup;
  message: string | undefined;
  formValid: boolean = true;
  users: UserResponseDto[] = [];
  roles: RoleDto[] = [];


  constructor(
    private usersService: UsersService,
    private rolesService: RolesService,
    private router: Router
  ) {
    this.form = addToRole;
  }

  ngOnInit() {
    this.usersService.apiUsersGet().subscribe({
      next: users => {
        this.users = users;
      },
      error: err => {
        console.log(err);
      }
    });

    this.rolesService.apiRolesGet().subscribe({
      next: roles => {
        this.roles = roles;
      },
      error: err => {
        console.log(err);
      }
    })
  }

  async deleteFromRole() {

    if (!this.form.valid) {
      return;
    }

    const addUserToRoleDto: AddUserToRoleDto = {
      userId: this.form.value.userId,
      roleId: this.form.value.roleId
    }

    this.usersService.apiUsersIdRolesDelete(this.form.value.userId, addUserToRoleDto).subscribe({
      next: result => {
        if (result) {
          this.formValid = true;
          this.message = '';
          this.form.reset();
          this.router.navigate(['/admin-dashboard']);
        }
      },
      error: err => {
        console.log(err);
        this.message = err.error.message;
        this.formValid = false;

        return;
      }
    })
  }
}
