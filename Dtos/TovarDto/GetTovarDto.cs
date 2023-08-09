using warehouse_project.Entities;

namespace warehouse_project.Dtos.TovarDto;
public class GetTovarDto
{
    public GetTovarDto(Tovar tovar)
    {
        Id = tovar.Id;
        Number = tovar.Number;
        NameTovar = tovar.NameTovar;
        Price = tovar.Price;
        CategoryId = tovar.CategoryId;
    }
    public Guid Id { get; set; }
    public string Number { get; set; }
    public string NameTovar { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
}