using Microsoft.EntityFrameworkCore;
using ShipmentService.Models;

namespace ShipmentService.dbConfig;

public class ShipmentDbContext : DbContext
{
    public ShipmentDbContext(DbContextOptions<ShipmentDbContext> options) : base (options) { }
    
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<ShipmentItem> ShipmentItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define relationships and constraints if needed
        modelBuilder.Entity<Shipment>()
            .HasMany(s => s.ShipmentItems)
            .WithOne(si => si.Shipment)
            .HasForeignKey(si => si.ShipmentId);

        // Other configuration if needed
    }
}