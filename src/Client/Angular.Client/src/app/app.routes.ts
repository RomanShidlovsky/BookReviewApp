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
import {AuthorUpdateComponent} from "./components/admin-dashboard/forms/author-update/author-update.component";
import {LanguageUpdateComponent} from "./components/admin-dashboard/forms/language-update/language-update.component";
import {SubjectUpdateComponent} from "./components/admin-dashboard/forms/subject-update/subject-update.component";
import {BookCreateComponent} from "./components/admin-dashboard/forms/book-create/book-create.component";

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
    path: 'admin-dashboard/book-create',
    component: BookCreateComponent,
  },
  {
    path: 'admin-dashboard/language-create',
    component: LanguageCreateComponent,
  },
  {
    path: 'admin-dashboard/language-update/:id',
    component: LanguageUpdateComponent
  },
  {
    path: 'admin-dashboard/subject-create',
    component: SubjectCreateComponent
  },
  {
    path: 'admin-dashboard/subject-update/:id',
    component: SubjectUpdateComponent
  },
  {
    path: 'admin-dashboard/author-create',
    component: AuthorCreateComponent
  },
  {
    path: 'admin-dashboard/author-update/:id',
    component: AuthorUpdateComponent
  },
];

