import {Component, OnInit} from '@angular/core';
import {FormGroup, ReactiveFormsModule} from "@angular/forms";
import {Router} from "@angular/router";
import {addToRole} from "../../../../forms/bookApi";
import {AddUserToRoleDto} from "../../../../models/user/addUserToRoleDto";
import {UsersService} from "../../../../api/users.service";
import {RolesService} from "../../../../api/roles.service";
import {UserResponseDto} from "../../../../models/review/userResponseDto";
import {RoleDto} from "../../../../models/role/roleDto";
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-add-to-role',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgForOf
  ],
  templateUrl: './add-to-role.component.html',
  styleUrl: './add-to-role.component.css'
})
export class AddToRoleComponent implements OnInit {
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

  async addToRole() {

    if (!this.form.valid) {
      return;
    }

    const addUserToRoleDto: AddUserToRoleDto = {
      userId: this.form.value.userId,
      roleId: this.form.value.roleId
    }

    this.usersService.apiUsersIdRolesPut(this.form.value.userId, addUserToRoleDto).subscribe({
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
