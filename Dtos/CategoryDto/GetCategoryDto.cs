using warehouse_project.Entities;

namespace warehouse_project.Dtos.CategoryDto;

public class GetCategoryDto
{
    public GetCategoryDto(Category category)
    {
        Id = category.Id;
        Name = category.Name;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
}