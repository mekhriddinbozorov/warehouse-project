namespace warehouse_project.Entities;
public class Tovar : IHasTime, IActive
{
    public Guid Id { get; set; }
    public string Number { get; set; }
    public string NameTovar { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool IsActive { get; set; }
    
    public virtual Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual List<Product> Products { get; set; }
}