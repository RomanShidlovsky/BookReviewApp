using MongoDB.Driver;
using Review.Domain.Entities;
using Review.Domain.Interfaces.Repositories;

namespace Review.Infrastructure.Repositories;

public class BookRepository(IMongoCollection<Book> collection) : BaseRepository<Book>(collection), IBookRepository;