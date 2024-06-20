using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.dbConfig;

public class AppDbContext: DbContext
{
    public DbSet<Order> Orders { get; set; }
    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }
}