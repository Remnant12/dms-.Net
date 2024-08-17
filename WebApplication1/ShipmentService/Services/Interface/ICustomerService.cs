namespace ShipmentService.Services.Implementation;

public interface ICustomerService
{
    Task<int?> GetCustomerIdByTokenAsync(string token);
}