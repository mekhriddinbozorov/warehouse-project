using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using warehouse_project.Data;
using warehouse_project.Dtos.DocumentDto;
using warehouse_project.Entities;

namespace warehouse_project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IAppDbContext dbContext;

    public DocumentsController(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDocument(
        [FromBody] CreateDocumentDto createDocumentDto,
        CancellationToken cancellationToken)
    {
        var document = dbContext.Documents.Add(new Document
        {
            Id = new Guid(),
            Name = createDocumentDto.Name,
            Date = createDocumentDto.Date,
            Provider = createDocumentDto.Provider,
            CategoryId = createDocumentDto.CategoryId
        });

        await dbContext.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(GetDocument), new { id = document.Entity.Id }, new GetDocumentDto(document.Entity));
    }

    [HttpGet]
    public async Task<IActionResult> GetDocuments(CancellationToken cancellationToken)
    {
        var documents = await dbContext.Documents
            .AsNoTracking()
            .Where(c => c.IsActive)
            .ToListAsync(cancellationToken);

        if (documents is null)
            return NotFound();

        return Ok(documents.Select(c => new GetDocumentDto(c)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDocument(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var document = await dbContext.Documents
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.IsActive && c.Id == id, cancellationToken);
        if (document is null)
            return NotFound();

        return Ok(new GetDocumentDto(document));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(
        [FromRoute] Guid id,
        [FromBody] UpdateDocumentDto updateDocument,
        CancellationToken cancellationToken)
    {
        var document = await dbContext.Documents
            .FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);

        if (document is null)
            return NotFound();

        document.Name = updateDocument.Name;
        document.Date = updateDocument.Date;
        document.Provider = updateDocument.Provider;
        document.CategoryId = updateDocument.CategoryId;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(document.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTovar(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var document = await dbContext.Documents
            .FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);

        if (document is null)
            return NotFound();

        document.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(document.Id);
    }
}