using WebApplication1.UserService.Models;

namespace UserAuthenticateService.Service.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(User user);
}