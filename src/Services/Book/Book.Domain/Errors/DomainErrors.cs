using System.Runtime.InteropServices.JavaScript;
using Shared;

namespace Book.Domain.Errors;

public class DomainErrors
{
    public static readonly Error InternalError = new(
        "Error.InternalError",
        "Internal error.",
        500);

    public static readonly Error UnknownError = new(
        "Unknown.UnknownError",
        "Unknown error occured");

    public static class Author
    {
        public static readonly Error AuthorNotFoundById = new(
            "Author.NotFoundById",
            "Author with specified id not found.",
            404);
        
        public static readonly Error AuthorNotFoundByOpenLibraryKey = new(
            "Author.NotFoundByOpenLibraryKey",
            "Author with specified open library key not found.",
            404);

        public static readonly Error OpenLibraryKeyConflict = new(
            "Author.OpenLibraryKeyConflict",
            "Author with provided OpenLibraryKey already exists.",
            409);

        public static readonly Error AuthorNotAddedToBook = new(
            "Author.AuthorNotAddedToBook",
            "Author not added to book",
            500);
        
        public static readonly Error AuthorNotRemovedFromBook = new(
            "Author.AuthorNotRemovedFromBook",
            "Author not removed from book",
            500);
    }

    public static class Book
    {
        public static readonly Error AlreadyContainsAuthor = new(
            "Book.AlreadyContainsAuthor",
            "Book already contains author.",
            400);

        public static readonly Error NotContainAuthor = new(
            "Book.NotContainAuthor",
            "Book does not contain author",
            400);

        public static readonly Error AlreadyContainsSubject = new(
            "Book.AlreadyContainsSubject",
            "Book already contains subject.",
            400);

        public static readonly Error NotContainSubject = new(
            "Book.NotContainSubject",
            "Book does not contain subject",
            400);
        
        public static readonly Error AlreadyContainsLanguage = new(
            "Book.AlreadyContainsLanguage",
            "Book already contains language.",
            400);

        public static readonly Error NotContainLanguage = new(
            "Book.NotContainLanguage",
            "Book does not contain language",
            400);
        
        public static readonly Error BookNotFoundById = new(
            "Book.NotFoundById",
            "Book with specified id not found.",
            404);

        public static readonly Error BookNotFoundByOpenLibraryKey = new(
            "Book.NotFoundByOpenLibraryKey",
            "Book with specified open library key not found.",
            404);

        public static readonly Error OpenLibraryKeyConflict = new(
            "Book.OpenLibraryKeyConflict",
            "Book with provided OpenLibraryKey already exists.",
            409);
    }

    public static class Subject
    {
        public static readonly Error SubjectNotFoundById = new(
            "Subject.NotFoundById",
            "Subject with specified id not found.",
            404);

        public static readonly Error SubjectNotFoundByName = new(
            "Subject.NotFoundByName",
            "Subject with specified name not found.",
            404);
        
        public static readonly Error NameConflict = new(
            "Subject.NameConflict",
            "Subject with provided name already exists.",
            409);
        
        public static readonly Error SubjectNotAddedToBook = new(
            "Subject.SubjectNotAddedToBook",
            "Subject not added to book",
            500);
        
        public static readonly Error SubjectNotRemovedFromBook = new(
            "Subject.SubjectNotRemovedFromBook",
            "Subject not removed from book",
            500);
    }
    
    public static class Language
    {
        public static readonly Error LanguageNotFoundById = new(
            "Language.NotFoundById",
            "Language with specified id not found.",
            404);
        
        public static readonly Error LanguageNotFoundByName = new(
            "Language.NotFoundByName",
            "Language with specified name not found.",
            404);

        public static readonly Error NameConflict = new(
            "Language.NameConflict",
            "Language with provided name already exists.",
            409);
        
        public static readonly Error LanguageNotAddedToBook = new(
            "Language.LanguageNotAddedToBook",
            "Language not added to book",
            500);
        
        public static readonly Error LanguageNotRemovedFromBook = new(
            "Language.LanguageNotRemovedFromBook",
            "Language not removed from book",
            500);
    }
}