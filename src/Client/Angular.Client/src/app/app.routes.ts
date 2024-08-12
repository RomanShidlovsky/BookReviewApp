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
import {FaqComponent} from "./components/faq/faq.component";
import {ContactComponent} from "./components/contact/contact.component";
import {BookFiltersComponent} from "./components/book-filters/book-filters.component";
import {UserProfileComponent} from "./components/user-profile/user-profile.component";
import {AddToRoleComponent} from "./components/admin-dashboard/forms/add-to-role/add-to-role.component";
import {DeleteFromRoleComponent} from "./components/admin-dashboard/forms/delete-from-role/delete-from-role.component";
import {authGuard} from "./guards/auth.guard";

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
    canActivate: [adminRoleGuard]
  },
  {
    path: 'admin-dashboard/book-create',
    component: BookCreateComponent,
    canActivate: [adminRoleGuard]
  },
  {
    path: 'admin-dashboard/language-create',
    component: LanguageCreateComponent,
    canActivate: [adminRoleGuard]
  },
  {
    path: 'admin-dashboard/language-update/:id',
    component: LanguageUpdateComponent,
    canActivate: [adminRoleGuard]
  },
  {
    path: 'admin-dashboard/subject-create',
    component: SubjectCreateComponent,
    canActivate: [adminRoleGuard]
  },
  {
    path: 'admin-dashboard/subject-update/:id',
    component: SubjectUpdateComponent,
    canActivate: [adminRoleGuard]
  },
  {
    path: 'admin-dashboard/author-create',
    component: AuthorCreateComponent,
    canActivate: [adminRoleGuard]
  },
  {
    path: 'admin-dashboard/author-update/:id',
    component: AuthorUpdateComponent,
    canActivate: [adminRoleGuard]
  },
  {
    path: 'admin-dashboard/add-to-role',
    component: AddToRoleComponent,
    canActivate: [adminRoleGuard]
  },
  {
    path: 'admin-dashboard/delete-from-role',
    component: DeleteFromRoleComponent,
    canActivate: [adminRoleGuard]
  },
  {
    path: 'faq',
    component: FaqComponent
  },
  {
    path: 'contact',
    component: ContactComponent
  },
  {
    path: 'filter',
    component: BookFiltersComponent
  },
  {
    path: 'user/:id',
    component: UserProfileComponent,
    canActivate: [authGuard]
  }
];

