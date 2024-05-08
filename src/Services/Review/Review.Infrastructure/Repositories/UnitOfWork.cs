using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Review.Domain.Entities;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Context;
using Review.Infrastructure.Interfaces;

namespace Review.Infrastructure.Repositories;

public class UnitOfWork(IMongoDbContext context, IOptions<ReviewDatabaseSettings> options, IDistributedCache cache) : IUnitOfWork
{
    private readonly Lazy<IBookRepository> _bookRepository =
        new(new BookRepository(context.GetCollection<Book>(options.Value.BooksCollectionName)));
    
    private readonly Lazy<IUserRepository> _userRepository =
        new(new UserRepository(context.GetCollection<User>(options.Value.UsersCollectionName)));
    
    private readonly Lazy<IReviewRepository> _reviewRepository =
        new(new ReviewRepository(context.GetCollection<ReviewEntity>(options.Value.ReviewsCollectionName),
            context.GetCollection<User>(options.Value.UsersCollectionName),
            context.GetCollection<Book>(options.Value.BooksCollectionName),
            cache));
    
    public IBookRepository BookRepository => _bookRepository.Value;
    public IUserRepository UserRepository => _userRepository.Value;
    public IReviewRepository ReviewRepository => _reviewRepository.Value;
}