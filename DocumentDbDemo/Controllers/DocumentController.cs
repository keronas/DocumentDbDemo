using DocumentDbDemo.Models;
using DocumentDbDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentDbDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<IEnumerable<StorageDocument>> GetAsync()
        {
            return await _documentService.GetAsync();
        }

        [HttpPost]
        [Route("CreateMockDocument")]
        public async Task PostAsync()
        {
            await _documentService.CreateAsync(new StorageDocument()
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
