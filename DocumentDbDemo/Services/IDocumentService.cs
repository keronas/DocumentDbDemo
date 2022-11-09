using DocumentDbDemo.Models;

namespace DocumentDbDemo.Services
{
    /// <summary>
    /// Service interface for creating and getting instances of <see cref="StorageDocument"/> from some storage.
    /// </summary>
    public interface IDocumentService
    {
        public Task<IEnumerable<StorageDocument>> GetAsync();

        public Task CreateAsync(StorageDocument newDocument);
    }
}
