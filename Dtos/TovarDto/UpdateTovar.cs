namespace warehouse_project.Dtos.TovarDto;
public class UpdateTovar
{
    public string Number { get; set; }
    public string NameTovar { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
}