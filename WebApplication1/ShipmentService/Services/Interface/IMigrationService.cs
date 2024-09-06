namespace ShipmentService.Services.Interface;

public interface IMigrationService
{
    Task ApplyNewSchemaChangesAsync();
}