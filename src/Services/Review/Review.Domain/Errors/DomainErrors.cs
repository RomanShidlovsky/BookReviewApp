using Shared;

namespace Review.Domain.Errors;

public class DomainErrors
{
    public static readonly Error InternalError = new(
        "Error.InternalError",
        "Internal error.",
        500);

    public static readonly Error UnknownError = new(
        "Unknown.UnknownError",
        "Unknown error occured");

    public static class Review
    {
        public static readonly Error ReviewNotFoundById = new(
            "Review.NotFoundById",
            "Review with specified id not found.",
            404);
    }
}