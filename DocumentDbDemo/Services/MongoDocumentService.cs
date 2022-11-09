using DocumentDbDemo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DocumentDbDemo.Services
{
    /// <summary>
    /// Service that gets and creates <see cref="StorageDocument"/> from a configured Mongo database.
    /// </summary>
    public class MongoDocumentService : IDocumentService
    {
        private readonly IMongoCollection<StorageDocument> _documentCollection;

        public MongoDocumentService(IOptions<MongoConnectionSettings> mongoConnectionSettings)
        {
            var mongoClient = new MongoClient(
                mongoConnectionSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoConnectionSettings.Value.DatabaseName);

            _documentCollection = mongoDatabase.GetCollection<StorageDocument>(
                mongoConnectionSettings.Value.DocumentCollectionName);
        }

        public async Task<IEnumerable<StorageDocument>> GetAsync() => await _documentCollection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(StorageDocument newDocument) => await _documentCollection.InsertOneAsync(newDocument);
    }
}
