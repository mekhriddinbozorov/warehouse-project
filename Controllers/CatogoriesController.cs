namespace warehouse_project.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using warehouse_project.Data;
using warehouse_project.Dtos.CategoryDto;
using warehouse_project.Entities;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IAppDbContext dbContext;

    public CategoriesController(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
    {
        var create = dbContext.Categories.Add(new Category
        {
            Id = new Guid(),
            Name = categoryDto.Name
        });

        await dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCategory), new { id = create.Entity.Id }, new GetCategoryDto(create.Entity));
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await dbContext.Categories
            .AsNoTracking()
            .Where(c => c.IsActive)
            .ToListAsync();

        if (categories is null)
            return NotFound();

        return Ok(categories.Select(c => new GetCategoryDto(c)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory([FromRoute] Guid id)
    {
        var category = await dbContext.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.IsActive && c.Id == id);
        if (category is null)
            return NotFound();

        return Ok(new GetCategoryDto(category));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(
        [FromRoute] Guid id,
        [FromBody] UpdateCategoryDto categoryDto)
    {
        var category = await dbContext.Categories
            .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);

        if (category is null)
            return NotFound();

        category.Name = categoryDto.Name;
        await dbContext.SaveChangesAsync();

        return Ok(category.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
    {
        var category = await dbContext.Categories
            .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);

        if (category is null)
            return NotFound();

        category.IsActive = false;
        await dbContext.SaveChangesAsync();

        return Ok(category.Id);
    }
}