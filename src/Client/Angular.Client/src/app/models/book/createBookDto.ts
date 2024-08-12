export interface CreateBookDto {
    openLibraryKey?: string | null;
    title: string;
    editionCount: number;
    publicationYear: number;
    imageUrl?: string;
}
