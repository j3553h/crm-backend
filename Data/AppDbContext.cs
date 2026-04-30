using Microsoft.EntityFrameworkCore;
using crm_backend.Models;

namespace crm_backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Activity> Activities => Set<Activity>();


protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Customer>()
        .HasOne(c => c.Company)
        .WithMany(c => c.Customers)
        .HasForeignKey(c => c.CompanyId)
        .IsRequired(false);

    modelBuilder.Entity<Activity>()
        .Property(a => a.Type)
        .HasConversion<string>();

    modelBuilder.Entity<Customer>()
        .HasMany(c => c.Activities)
        .WithOne(a => a.Customer)
        .HasForeignKey(a => a.CustomerId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Customer>().HasData(
        new Customer { Id = 1, Name = "Alice Johnson", Email = "alice@demo.com", CompanyId = null },
        new Customer { Id = 2, Name = "Bob Smith", Email = "bob@demo.com", CompanyId = null  },
        new Customer { Id = 3, Name = "Charlie Brown", Email = "charlie@demo.com", CompanyId = null  }
    );

    modelBuilder.Entity<Company>().HasData(
        new Company { Id = 1, Name = "Acme Corp" },
        new Company { Id = 2, Name = "Globex Inc" }
    );

    modelBuilder.Entity<Activity>().HasData(
        new Activity
        {
            Id = 1,
            CustomerId = 1,
            Type = Enums.ActivityType.Call,
            Description = "Intro call with customer",
            CreatedAt = new DateTime(2026, 4, 29, 0, 0, 0, DateTimeKind.Utc)
        },
        new Activity
        {
            Id = 2,
            CustomerId = 2,
            Type = Enums.ActivityType.Email,
            Description = "Sent pricing proposal",
            CreatedAt = new DateTime(2026, 4, 15, 0, 0, 0, DateTimeKind.Utc)
        }
    );
}

}

