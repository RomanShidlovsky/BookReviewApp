export interface UpdateAuthorDto {
    id: number;
    openLibraryKey: string | null;
    firstName: string;
    lastName: string;
    fullName: string;
    birthDate: string;
    deathDate: string | null;
    biography: string | null;
    imageUrl: string | null;
}
