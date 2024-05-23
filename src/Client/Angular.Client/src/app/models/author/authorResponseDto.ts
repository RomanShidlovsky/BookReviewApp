export interface AuthorResponseDto {
    id: number;
    openLibraryKey?: string | null;
    firstName: string;
    lastName: string;
    fullName: string;
    birthDate: string;
    deathDate?: string;
    biography?: string;
    imageUrl?: string;
}
