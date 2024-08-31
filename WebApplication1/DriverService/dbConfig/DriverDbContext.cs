using Microsoft.EntityFrameworkCore;

namespace Driver.dbConfig;

public class DriverDbContext : DbContext
{
    public DbSet<Models.Driver> Drivers { get; set; }

    public DriverDbContext(DbContextOptions<DriverDbContext> options) : base(options)
    {
        
    }
}