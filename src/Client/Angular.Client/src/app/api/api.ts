export * from './authors.service';
import { AuthorsService } from './authors.service';
export * from './books.service';
import { BooksService } from './books.service';
export * from './languages.service';
import { LanguagesService } from './languages.service';
export * from './subjects.service';
import { SubjectsService } from './subjects.service';
export * from './users.service';
import { UsersService} from "./users.service";
export * from './roles.service';
import { RolesService} from "./roles.service";

export const APIS = [AuthorsService, BooksService, LanguagesService, SubjectsService, UsersService, RolesService];
