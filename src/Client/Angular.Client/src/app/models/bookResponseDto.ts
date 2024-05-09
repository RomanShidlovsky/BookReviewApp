import {AuthorResponseDto} from "./authorResponseDto";
import {LanguageResponseDto} from "./languageResponseDto";
import {SubjectResponseDto} from "./subjectResponseDto";

export interface BookResponseDto {
  id: number;
  openLibraryKey?: string | null;
  title: string;
  editionCount: number;
  publicationYear: number;
  averageRating: number;
  imageUrl?: string | null;
  authors: AuthorResponseDto[];
  languages: LanguageResponseDto[];
  subjects: SubjectResponseDto[];
}
