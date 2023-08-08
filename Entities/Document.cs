namespace warehouse_project.Entities;

public class Document : IHasTime
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Provider { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public virtual Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual List<Product> Products { get; set; }
}