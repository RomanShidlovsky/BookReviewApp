using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Review.Domain.Entities;
using Review.Infrastructure.Interfaces;

namespace Review.Infrastructure.Context;

public class ReviewContext(IOptions<ReviewDatabaseSettings> options, IMongoClient client) : IMongoDbContext
{
    private readonly IMongoDatabase _db = client.GetDatabase(options.Value.DatabaseName);

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _db.GetCollection<T>(collectionName);
    }
}