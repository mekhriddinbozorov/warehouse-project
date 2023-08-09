namespace warehouse_project.Entities;

public class ProductMedium : IHasTime
{
     public Guid Id { get; set; }
    public string MimeType { get; set; }
    public string Filename { get; set; }
    public string Extension{ get; set; }
    public int Order { get; set; }
    public byte[] Data { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public virtual Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
}