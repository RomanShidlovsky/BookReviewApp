export interface UpdateBookDto {
    id: number;
    openLibraryKey?: string | null;
    title: string;
    editionCount: number;
    publicationYear: number;
    imageUrl?: string | null;
}
