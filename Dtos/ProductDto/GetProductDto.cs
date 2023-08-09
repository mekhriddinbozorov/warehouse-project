using warehouse_project.Entities;

namespace warehouse_project.Dtos.ProductDto;
public class GetProductDto
{
    public GetProductDto(Product product)
    {
        Id = product.Id;
        Quantity = product.Quantity;
        DocumentId = product.DocumentId;
        TovarId = product.TovarId;
    }
    public Guid Id { get; set; }
    public decimal Quantity { get; set; }
    public Guid DocumentId { get; set; }
    public Guid TovarId { get; set; }
}