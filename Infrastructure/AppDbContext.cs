using DomainModel.Entities.Vessels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Vessel> Vessels { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vessel>()
            .HasKey(v => v.Id);
        
        modelBuilder.Entity<Vessel>()
            .HasIndex(v => v.Id)
            .IsUnique();

        modelBuilder.Entity<Vessel>()
            .OwnsOne(v => v.Imo);
    }
}