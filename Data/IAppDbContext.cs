using Microsoft.EntityFrameworkCore;
using warehouse_project.Entities;

namespace warehouse_project.Data;
public interface IAppDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Contact> Contacts { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Document> Documents { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ProductMedium> ProductMedia { get; set; }
    DbSet<Tovar> Tovars { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}