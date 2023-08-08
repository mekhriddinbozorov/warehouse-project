namespace warehouse_project.Entities;

public class Product : IHasTime, IActive
{
    public Guid Id { get; set; }
    public decimal Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool IsActive { get; set; }
    
    public virtual Guid DocumentId { get; set; }
    public virtual Document Document { get; set; }
    public virtual Guid TovarId { get; set; }
    public virtual Tovar Tovar { get; set; }
    public virtual List<ProductMedium> ProductMedia { get; set; }
}