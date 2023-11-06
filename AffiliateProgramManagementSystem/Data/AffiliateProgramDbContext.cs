using Microsoft.EntityFrameworkCore;

namespace AffiliateProgramManagementSystem.Data;

/// <summary>
/// Affiliate DB Context
/// </summary>
public class AffiliateProgramDbContext : DbContext
{
    /// <summary>
    /// Affiliate Entity.
    /// </summary>
    public DbSet<Affiliate> Affiliates { get; set; }
    /// <summary>
    /// Customer Entity.
    /// </summary>
    public DbSet<Customer> Customers { get; set; }
    /// <summary>
    /// Database file path.
    /// </summary>
    public string? DbPath { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    public AffiliateProgramDbContext(DbContextOptions<AffiliateProgramDbContext> options)
       : base(options)
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public AffiliateProgramDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "affiliateprogrammanagementsystem.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Affiliate>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Customer>()
            .Property(f => f.AffiliateID)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Affiliate>()
            .Navigation(e => e.Customers)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        modelBuilder.Entity<Customer>()
            .Navigation(e => e.Affiliate)
            .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}
