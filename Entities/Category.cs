namespace warehouse_project.Entities;
public class Category : IHasTime
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public virtual List<Tovar> Tovars { get; set; }
    public virtual List<Document> Documents { get; set; }
}