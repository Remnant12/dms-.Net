using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using ShipmentService.dbConfig;
using ShipmentService.Services.Interface;

namespace ShipmentService.Services.Implementation;

public class MigrationService : IMigrationService
{
    private readonly ShipmentDbContext _context;
    
    public MigrationService(ShipmentDbContext context)
    {
        _context = context; 
    }
    
    public async Task ApplyNewSchemaChangesAsync()
    {
        await _context.Database.MigrateAsync();
    }
    
    private IEnumerable<string> GetAppliedMigrations()
    {
        return _context.Database.GetAppliedMigrations();
    }
    
    private string GenerateMigrationScript()
    {
        // Generate the migration script
        var optionsBuilder = new DbContextOptionsBuilder<ShipmentDbContext>();
        optionsBuilder.UseMySql(_context.Database.GetDbConnection().ConnectionString, ServerVersion.AutoDetect(_context.Database.GetDbConnection().ConnectionString));

        using (var context = new ShipmentDbContext(optionsBuilder.Options))
        {
            var migrations = context.Database.GetMigrations();
            var script = context.Database.GenerateCreateScript();
            return script;
        }
    }
    
    public IEnumerable<string> GetPendingMigrations()
    {
        return _context.Database.GetPendingMigrations();
    }
    
}