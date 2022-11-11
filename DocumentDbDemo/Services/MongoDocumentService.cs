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

        // To be potentially used for unit testing this class
        public MongoDocumentService(IMongoCollection<StorageDocument> mongoCollection)
        {
            _documentCollection = mongoCollection;
        }

        public async Task<IEnumerable<StorageDocument>> GetAsync() => await _documentCollection.Find(_ => true).ToListAsync();

        public async Task<StorageDocument?> GetAsync(string id) => await _documentCollection.Find(document => document.Id == id).SingleOrDefaultAsync();

        public async Task CreateAsync(StorageDocument newDocument)
        {
            if (await _documentCollection.Find(document => document.Id == newDocument.Id).AnyAsync())
            {
                throw new ArgumentException("Document with this ID already exists.");
            }
            await _documentCollection.InsertOneAsync(newDocument);
        }

        public async Task UpdateAsync(string id, StorageDocument updatedDocument)
        {
            if (updatedDocument.Id != id)
            {
                throw new ArgumentException("New document must have the same ID as specified.");
            }
            else if (!await _documentCollection.Find(document => document.Id == id).AnyAsync())
            {
                throw new ArgumentException("Document with the specified ID does not exist.");
            }

            await _documentCollection.ReplaceOneAsync(document => document.Id == id, updatedDocument);
        }
    }
}
