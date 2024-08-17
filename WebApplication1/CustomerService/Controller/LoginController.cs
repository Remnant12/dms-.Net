using CustomerService.Models;
using CustomerService.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CustomerService.Controller;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly CustomerDbContext _context;
    private readonly IJwtService _jwtService;

    public LoginController(CustomerDbContext context, IJwtService jwtService)
    {
        _context = context; 
        _jwtService = jwtService; 
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] Login loginModel)
    {
        var user = _context.Customers.SingleOrDefault(u => u.Email == loginModel.Email);

        if (user == null)
        {
            return Unauthorized("User not found");
        }

        if (!BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password))
        {
            return Unauthorized("Invalid password");
        }

        var token = _jwtService.GenerateJwtToken(user);

        return Ok(new { Token = token });
    }
}