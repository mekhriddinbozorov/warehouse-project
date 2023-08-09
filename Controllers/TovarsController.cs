using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using warehouse_project.Data;
using warehouse_project.Dtos.TovarDto;
using warehouse_project.Entities;

namespace warehouse_project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TovarsController : ControllerBase
{
    private readonly IAppDbContext dbContext;

    public TovarsController(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTovar([FromBody] CreateTovarDto tovarDto)
    {
        var tovar = dbContext.Tovars.Add(new Tovar
        {
            Id = new Guid(),
            Number = tovarDto.Number,
            NameTovar = tovarDto.NameTovar,
            Price = tovarDto.Price,
            CategoryId = tovarDto.CategoryId
        });
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTovar), new { id = tovar.Entity.Id }, new GetTovarDto(tovar.Entity));
    }

    [HttpGet]
    public async Task<IActionResult> GetTovars()
    {
        var tovars = await dbContext.Tovars
            .AsNoTracking()
            .Where(c => c.IsActive).ToListAsync();

        if (tovars is null)
            return NotFound();

        return Ok(tovars.Select(c => new GetTovarDto(c)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTovar([FromRoute] Guid id)
    {
        var tovar = await dbContext.Tovars
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.IsActive && c.Id == id);

        if (tovar is null)
            return NotFound();

        return Ok(new GetTovarDto(tovar));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTovar([FromRoute] Guid id, [FromBody] UpdateTovar tovarDto)
    {
        var tovar = await dbContext.Tovars.FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        if (tovar is null)
            return NotFound();
        tovar.Number = tovarDto.Number;
        tovar.NameTovar = tovarDto.NameTovar;
        tovar.Price = tovarDto.Price;
        tovar.CategoryId = tovarDto.CategoryId;

        await dbContext.SaveChangesAsync();
        return Ok(tovar.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTovar([FromRoute] Guid id)
    {
        var tovar = await dbContext.Tovars.FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        if (tovar is null)
            return NotFound();
        tovar.IsActive = false;
        await dbContext.SaveChangesAsync();
        return Ok(tovar.Id);
    }
}