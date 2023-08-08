using Microsoft.EntityFrameworkCore;
using warehouse_project.Entities;

namespace warehouse_project.Data;
public interface IAppDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Contact> Contacts { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}