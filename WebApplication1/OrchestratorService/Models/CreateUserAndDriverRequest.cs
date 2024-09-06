using DefaultNamespace;
using DriverService1.DTO;

namespace OrchestratorService.Models;

public class CreateUserAndDriverRequest
{
    public CreateUserDto CreateUserDto { get; set; }
    public CreateDriverDTO CreateDriverDto { get; set; }
}