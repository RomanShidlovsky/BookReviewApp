namespace Review.Infrastructure.Context;

public class ReviewDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ReviewsCollectionName { get; set; } = null!;
    public string UsersCollectionName { get; set; } = null!;
    public string BooksCollectionName { get; set; } = null!;
}