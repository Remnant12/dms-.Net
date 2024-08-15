using CustomerService.Models;
using Microsoft.EntityFrameworkCore;

public class CustomerDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the Customer model (if necessary)
        base.OnModelCreating(modelBuilder);
    }
}