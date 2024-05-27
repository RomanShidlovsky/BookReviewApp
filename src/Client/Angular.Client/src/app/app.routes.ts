import {Routes} from '@angular/router';
import {HomeComponent} from "./components/home/home.component";
import {RegisterComponent} from "./components/register/register.component";
import {LoginComponent} from "./components/login/login.component";
import {BookComponent} from "./components/book/book.component";
import {AdminDashboardComponent} from "./components/admin-dashboard/admin-dashboard.component";
import {LanguageCreateComponent} from "./components/admin-dashboard/forms/language-create/language-create.component";
import {SubjectCreateComponent} from "./components/admin-dashboard/forms/subject-create/subject-create.component";
import {adminRoleGuard} from "./guards/admin-role.guard";
import {AuthorCreateComponent} from "./components/admin-dashboard/forms/author-create/author-create.component";

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'book/:id',
    component: BookComponent
  },
  {
    path: 'admin-dashboard',
    component: AdminDashboardComponent,
  },
  {
    path: 'admin-dashboard/language-create',
    component: LanguageCreateComponent,
  },
  {
    path: 'admin-dashboard/subject-create',
    component: SubjectCreateComponent
  },
  {
    path: 'admin-dashboard/author-create',
    component: AuthorCreateComponent
  }
];

