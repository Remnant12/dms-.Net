using Microsoft.AspNetCore.Mvc;
using OrchestratorService.Models;
using OrchestratorService.Services;

namespace OrchestratorService.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserDriverController : ControllerBase
{
    private readonly UserDriverService _userDriverService;
    
    public UserDriverController(UserDriverService userDriverService)
    {
        _userDriverService = userDriverService;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateUserAndDriver([FromBody] CreateUserAndDriverRequest request)
    {
        try
        {
            await _userDriverService.CreateUserAndDriver(request.CreateUserDto, request.CreateDriverDto);
            return Ok("User and Driver created successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}