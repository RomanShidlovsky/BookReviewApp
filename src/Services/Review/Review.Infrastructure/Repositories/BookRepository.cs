using Review.Domain.Entities;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Context;

namespace Review.Infrastructure.Repositories;

public class BookRepository(ReviewContext context) : BaseRepository<Book>(context), IBookRepository;