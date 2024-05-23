import {AuthorResponseDto} from "../author/authorResponseDto";
import {LanguageResponseDto} from "../language/languageResponseDto";
import {SubjectResponseDto} from "../subject/subjectResponseDto";
import {Review} from "../review";

export interface BookResponseDto {
    id: number;
    openLibraryKey?: string | null;
    title: string;
    editionCount: number;
    publicationYear: number;
    averageRating: number;
    averageCriticRating: number;
    imageUrl?: string | null;
    authors: Array<AuthorResponseDto>;
    languages: Array<LanguageResponseDto>;
    subjects: Array<SubjectResponseDto>;
}

export interface BookWithReviewsResponseDto {
  id: number;
  openLibraryKey?: string | null;
  title: string;
  editionCount: number;
  publicationYear: number;
  averageRating: number;
  averageCriticRating: number;
  imageUrl?: string | null;
  authors: Array<AuthorResponseDto>;
  languages: Array<LanguageResponseDto>;
  subjects: Array<SubjectResponseDto>;
  reviews: Array<Review>;
  criticReviews: Array<Review>;
}
