namespace warehouse_project.Data;
using Microsoft.EntityFrameworkCore;
using warehouse_project.Entities;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductMedium> ProductMedia { get; set; }
    public DbSet<Tovar> Tovars { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User entity
        modelBuilder.Entity<User>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<User>()
            .Property(e => e.Fullname)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(e => e.Username)
            .HasMaxLength(30)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(e => e.Language)
            .HasMaxLength(10)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(e => e.Phone)
            .HasMaxLength(20);

        // User and Contact (one to one relationship)
        modelBuilder.Entity<User>()
            .HasOne(c => c.Contact)
            .WithOne(u => u.User)
            .HasForeignKey<Contact>(u => u.UserId)
            .IsRequired();

        // Contact entity
        modelBuilder.Entity<Contact>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<Contact>()
            .Property(u => u.PhoneNumber)
            .HasMaxLength(20);

        modelBuilder.Entity<Contact>()
            .Property(u => u.FirstName)
            .HasMaxLength(25);

        modelBuilder.Entity<Contact>()
            .Property(u => u.LastName)
            .HasMaxLength(25);

        modelBuilder.Entity<Contact>()
            .Property(u => u.Vcard)
            .HasMaxLength(32);

        // Category entity
        modelBuilder.Entity<Category>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Category>()
            .HasMany(d => d.Documents)
            .WithOne(c => c.Category)
            .HasForeignKey(c => c.CategoryId)
            .IsRequired();

        modelBuilder.Entity<Category>()
            .HasMany(d => d.Tovars)
            .WithOne(c => c.Category)
            .HasForeignKey(c => c.CategoryId)
            .IsRequired();

        // Document entity
        modelBuilder.Entity<Document>()
            .HasKey(d => d.Id);

        modelBuilder.Entity<Document>()
            .Property(d => d.Name)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Document>()
            .Property(d => d.Date)
            .IsRequired();

        modelBuilder.Entity<Document>()
            .Property(d => d.Provider)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Document>()
            .HasMany(d => d.Products)
            .WithOne(c => c.Document)
            .HasForeignKey(c => c.DocumentId)
            .IsRequired();

        // Product entity
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Product>()
            .Property(p => p.Quantity)
            .HasPrecision(10, 2)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .HasMany(p => p.ProductMedia)
            .WithOne(m => m.Product)
            .HasForeignKey(m => m.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Tovar
        modelBuilder.Entity<Tovar>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Tovar>()
            .Property(t => t.Number)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<Tovar>()
            .Property(t => t.NameTovar)
            .HasMaxLength(1500)
            .IsRequired();

        modelBuilder.Entity<Tovar>()
            .Property(p => p.Price)
            .HasPrecision(12, 2)
            .IsRequired();

        modelBuilder.Entity<ProductMedium>()
            .HasKey(c => c.Id);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetDates();
        SetDatesIsActive();

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

    private void SetDatesIsActive()
    {
        foreach (var entry in ChangeTracker.Entries<IActive>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.IsActive = true;
            }

            if (entry.State == EntityState.Deleted)
            {
                entry.Entity.IsActive = false;
            }
        }
    }
}