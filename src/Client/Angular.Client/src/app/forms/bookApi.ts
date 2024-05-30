import { FormGroup, FormControl, Validators } from '@angular/forms';

export const addAuthorToBookDtoForm = new FormGroup({
  authorId: new FormControl(null, []),
  bookId: new FormControl(null, [])
});

export const addLanguageToBookDtoForm = new FormGroup({
  languageId: new FormControl(null, []),
  bookId: new FormControl(null, [])
});

export const addSubjectToBookDtoForm = new FormGroup({
  subjectId: new FormControl(null, []),
  bookId: new FormControl(null, [])
});

export const authorResponseDtoForm = new FormGroup({
  id: new FormControl(null, []),
  openLibraryKey: new FormControl(null, []),
  firstName: new FormControl(null, []),
  lastName: new FormControl(null, []),
  fullName: new FormControl(null, []),
  birthDate: new FormControl(null, []),
  deathDate: new FormControl(null, []),
  biography: new FormControl(null, []),
  imageUrl: new FormControl(null, [])
});

export const bookResponseDtoForm = new FormGroup({
  id: new FormControl(null, []),
  openLibraryKey: new FormControl(null, []),
  title: new FormControl(null, []),
  editionCount: new FormControl(null, []),
  publicationYear: new FormControl(null, []),
  averageRating: new FormControl(null, []),
  averageCriticRating: new FormControl(null, []),
  imageUrl: new FormControl(null, []),
  authors: new FormControl(null, []),
  languages: new FormControl(null, []),
  subjects: new FormControl(null, [])
});

export const createAuthorDtoForm = new FormGroup({
  openLibraryKey: new FormControl(null, []),
  firstName: new FormControl(null, [Validators.required]),
  lastName: new FormControl(null, [Validators.required]),
  fullName: new FormControl(null, [Validators.required]),
  birthDate: new FormControl(null, [Validators.required]),
  deathDate: new FormControl(null, []),
  biography: new FormControl(null, []),
  imageUrl: new FormControl(null, [])
});

export const createBookDtoForm = new FormGroup({
  openLibraryKey: new FormControl('', []),
  title: new FormControl(null, [Validators.required]),
  editionCount: new FormControl(null, [Validators.required]),
  publicationYear: new FormControl(2000, [Validators.required]),
  selectedLanguages: new FormControl([], []),
  selectedSubjects: new FormControl([], []),
  selectedAuthors: new FormControl([], [])
});

export const createLanguageDtoForm = new FormGroup({
  name: new FormControl(null, [Validators.required, Validators.minLength(2)])
});

export const createSubjectDtoForm = new FormGroup({
  name: new FormControl(null, [Validators.required, Validators.minLength(2)])
});

export const errorForm = new FormGroup({
  code: new FormControl(null, []),
  message: new FormControl(null, []),
  errorStatusCode: new FormControl(null, [])
});

export const languageResponseDtoForm = new FormGroup({
  id: new FormControl(null, []),
  name: new FormControl(null, [])
});

export const removeAuthorFromBookDtoForm = new FormGroup({
  authorId: new FormControl(null, []),
  bookId: new FormControl(null, [])
});

export const removeLanguageFromBookDtoForm = new FormGroup({
  languageId: new FormControl(null, []),
  bookId: new FormControl(null, [])
});

export const removeSubjectFromBookDtoForm = new FormGroup({
  subjectId: new FormControl(null, []),
  bookId: new FormControl(null, [])
});

export const subjectResponseDtoForm = new FormGroup({
  id: new FormControl(null, []),
  name: new FormControl(null, [])
});

export const updateAuthorDtoForm = new FormGroup({
  id: new FormControl(null, [Validators.required]),
  openLibraryKey: new FormControl(null, []),
  firstName: new FormControl(null, [Validators.required]),
  lastName: new FormControl(null, [Validators.required]),
  fullName: new FormControl(null, [Validators.required]),
  birthDate: new FormControl(null, [Validators.required]),
  deathDate: new FormControl(null, []),
  biography: new FormControl(null, []),
  imageUrl: new FormControl(null, [])
});

export const updateBookDtoForm = new FormGroup({
  id: new FormControl(null, [Validators.required]),
  openLibraryKey: new FormControl(null, []),
  title: new FormControl(null, [Validators.required]),
  editionCount: new FormControl(null, [Validators.required]),
  publicationYear: new FormControl(null, [Validators.required]),
  imageUrl: new FormControl(null, [])
});

export const updateLanguageDtoForm = new FormGroup({
  id: new FormControl(null, [Validators.required]),
  name: new FormControl(null, [Validators.required])
});

export const updateSubjectDtoForm = new FormGroup({
  id: new FormControl(null, [Validators.required]),
  name: new FormControl(null, [Validators.required])
});
