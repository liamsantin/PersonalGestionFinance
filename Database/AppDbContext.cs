using ApiPersonalGestionFinance.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonalGestionFinance.Database;

public class AppDbContext : DbContext
{
    
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Table User
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("TA_USER");
            entity.HasKey(u => u.UserId);

            // Relation User → Address (many-to-one)
            entity.HasOne(u => u.Address)
                  .WithMany()               // une adresse peut être liée à plusieurs users
                  .HasForeignKey(u => u.AddressId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Table Address
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("TA_ADDRESS");
            entity.HasKey(a => a.AddressId);
        });
    }
}
