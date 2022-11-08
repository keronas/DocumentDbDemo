using DocumentDbDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DocumentDbDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IMongoCollection<StorageDocument> _documentCollection;

        public DocumentsController(IOptions<MongoConnectionSettings> mongoConnectionSettings)
        {
            var mongoClient = new MongoClient(
                mongoConnectionSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoConnectionSettings.Value.DatabaseName);

            _documentCollection = mongoDatabase.GetCollection<StorageDocument>(
                mongoConnectionSettings.Value.DocumentCollectionName);
        }

        [HttpGet]
        public async Task<IEnumerable<StorageDocument>> GetAsync()
        {
            return await _documentCollection.Find(_ => true).ToListAsync();
        }

        [HttpPost(Name = "CreateMockDocument")]
        public async Task PostAsync()
        {
            await _documentCollection.InsertOneAsync(new StorageDocument()
            {
                Id = Guid.NewGuid().ToString(),
                Tags = new[] { "mock", "" },
                Data = new StorageDocumentData()
                {
                    ArbitraryStringField = "aaa",
                    ArbitraryIntField = 42,
                    ArbitraryBoolField = true
                }
            });
        }
    }
}
