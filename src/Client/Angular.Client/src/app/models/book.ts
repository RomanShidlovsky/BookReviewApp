import { Author } from "./author";
import {Language} from "./language";
import {Subject} from "./subject";

export interface Book {
  id: number;
  openLibraryKey?: string | null;
  title: string;
  editionCount: number;
  publicationYear: number;
  averageRating: number;
  imageUrl?: string | null;
  authors: Author[];
  languages: Language[];
  subjects: Subject[];
}
