namespace warehouse_project.Data;
using Microsoft.EntityFrameworkCore;
using warehouse_project.Entities;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<User>()
            .Property(e => e.Fullname)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(e => e.Username)
            .HasMaxLength(50);

        modelBuilder.Entity<User>()
            .Property(e => e.Language)
            .HasMaxLength(10);

        modelBuilder.Entity<User>()
            .Property(e => e.Phone)
            .HasMaxLength(20);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Contact)
            .WithOne(c => c.User)
            .HasPrincipalKey<User>(u => u.Id)
            .HasForeignKey<Contact>(c => c.UserId);

        modelBuilder.Entity<Contact>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<Contact>()
            .Property(u => u.PhoneNumber)
            .HasMaxLength(20);


        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetDates();

        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetDates()
    {
        foreach (var entry in ChangeTracker.Entries<IHasTime>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.ModifiedAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedAt = DateTime.UtcNow;
            }
        }
    }
}