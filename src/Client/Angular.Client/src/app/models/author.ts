export interface Author {
  id: number;
  openLibraryKey?: string | null;
  firstName: string;
  lastName: string;
  fullName: string;
  birthDate?: string | null;
  deathDate?: string | null;
  biography?: string | null;
  imageUrl?: string | null;
}
