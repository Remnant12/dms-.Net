using Microsoft.EntityFrameworkCore;
using VehicleService.Models;

namespace VehicleService.dbConfig;

public class VehicleDbContext : DbContext
{
    public DbSet<Vehicle> Vehicles { get; set; }

    public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
    {
        
    }
}