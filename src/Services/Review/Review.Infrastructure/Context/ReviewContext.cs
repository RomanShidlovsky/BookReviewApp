using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Review.Domain.Entities;

namespace Review.Infrastructure.Context;

public class ReviewContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ReviewEntity> Reviews { get; init; }
    public DbSet<User> Users { get; init; }
    public DbSet<Book> Books { get; init; }

    public static ReviewContext Create(IMongoDatabase database)
    {
        return new(new DbContextOptionsBuilder<ReviewContext>()
                    .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
                    .Options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}