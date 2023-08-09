namespace warehouse_project.Entities;
public class Category : IHasTime, IActive
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool IsActive { get; set; }
    
    public virtual ICollection<Tovar> Tovars { get; set; }
    public virtual ICollection<Document> Documents { get; set; }
}