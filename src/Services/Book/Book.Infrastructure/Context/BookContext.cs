using System.Reflection;
using Book.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BookEntity = Book.Domain.Entities.Book;

namespace Book.Infrastructure.Context;

public class BookContext(DbContextOptions<BookContext> options) : DbContext(options)
{
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<BookEntity> Books { get; set; } = null!;
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<Language> Languages { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}