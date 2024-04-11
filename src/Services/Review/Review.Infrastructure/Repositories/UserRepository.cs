using Review.Domain.Entities;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Context;

namespace Review.Infrastructure.Repositories;

public class UserRepository(ReviewContext context) : BaseRepository<User>(context), IUserRepository;
