using Review.Domain.Interfaces.Repositories;

namespace Review.Infrastructure.Repositories;

public interface IUnitOfWork
{
    IReviewRepository ReviewRepository { get; }
    IUserRepository UserRepository { get; }
    IBookRepository BookRepository { get; }
}