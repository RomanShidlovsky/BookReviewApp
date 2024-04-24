using MongoDB.Driver;

namespace Review.Infrastructure.Interfaces;

public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>(string collectionName);
}