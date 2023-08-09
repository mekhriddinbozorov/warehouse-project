namespace warehouse_project.Dtos.ProductDto;
public class CreateProductDto
{
    public decimal Quantity { get; set; }
    public Guid DocumentId { get; set; }
    public Guid TovarId { get; set; }
}