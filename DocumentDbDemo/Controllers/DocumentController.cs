using DocumentDbDemo.Models;
using DocumentDbDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentDbDemo.Controllers
{
    /// <summary>
    /// Controller for working with <see cref="StorageDocument"/>.
    /// </summary>
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StorageDocument>))]
        public async Task<IEnumerable<StorageDocument>> GetAsync()
        {
            return await _documentService.GetAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StorageDocument))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StorageDocument>> GetAsync(string id)
        {
            var document = await _documentService.GetAsync(id);
            return document is not null ? document : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StorageDocument))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync(StorageDocument document)
        {
            try
            {
                await _documentService.CreateAsync(document);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            
            var getActionName = nameof(GetAsync).Replace("Async", ""); // Async suffix is trimmed from action names by default, so this is needed for it to match
            return CreatedAtAction(getActionName, new { id = document.Id }, document);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync(string id, StorageDocument document)
        {
            try
            {
                await _documentService.UpdateAsync(id, document);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }
    }
}
