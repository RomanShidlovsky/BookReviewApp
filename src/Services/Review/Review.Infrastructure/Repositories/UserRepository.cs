using MongoDB.Driver;
using Review.Domain.Entities;
using Review.Domain.Interfaces.Repositories;

namespace Review.Infrastructure.Repositories;

public class UserRepository(IMongoCollection<User> collection) : BaseRepository<User>(collection), IUserRepository;
