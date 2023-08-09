using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using warehouse_project.Data;
using warehouse_project.Dtos.ProductDto;
using warehouse_project.Entities;

namespace warehouse_project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IAppDbContext dbContext;

    public ProductsController(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDocument([FromBody] CreateProductDto createProductDto)
    {
        var product = dbContext.Products.Add(new Product
        {
            Id = new Guid(),
            Quantity = createProductDto.Quantity,
            DocumentId = createProductDto.DocumentId,
            TovarId = createProductDto.TovarId
        });

        await dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProduct), new { id = product.Entity.Id }, new GetProductDto(product.Entity));
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var product = await dbContext.Products
            .AsNoTracking()
            .Where(c => c.IsActive)
            .ToListAsync();

        if (product is null)
            return NotFound();

        return Ok(product.Select(c => new GetProductDto(c)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id)
    {
        var product = await dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.IsActive && c.Id == id);
        if (product is null)
            return NotFound();

        return Ok(new GetProductDto(product));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] Guid id,
        [FromBody] UpdateProductDto updateProductDto)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);

        if (product is null)
            return NotFound();

        product.Quantity = updateProductDto.Quantity;
        product.DocumentId = updateProductDto.DocumentId;
        product.TovarId = updateProductDto.TovarId;

        await dbContext.SaveChangesAsync();

        return Ok(product.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);

        if (product is null)
            return NotFound();

        product.IsActive = false;
        await dbContext.SaveChangesAsync();

        return Ok(product.Id);
    }
}