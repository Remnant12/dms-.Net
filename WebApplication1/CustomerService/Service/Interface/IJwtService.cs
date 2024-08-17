using CustomerService.Models;

namespace CustomerService.Service.Interface;

public interface IJwtService
{
    string GenerateJwtToken(Customer customer);
}