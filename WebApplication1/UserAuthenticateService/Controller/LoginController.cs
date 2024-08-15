
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAuthenticateService.Service.Implementation;
using UserAuthenticateService.Service.Interfaces;
using UserService.Data;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly UserDbContext _context;
    private readonly ITokenBlacklistService _tokenBlacklistService;
    private readonly IJwtService _jwtService;

    public LoginController(UserDbContext context, ITokenBlacklistService tokenBlacklistService, IJwtService jwtService)
    {
        _context = context;
        _tokenBlacklistService = tokenBlacklistService;
        _jwtService = jwtService; 
    }

    
    [HttpPost("login")]
    public IActionResult Login([FromBody] Login loginModel)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == loginModel.Email);

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
    
    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
        _tokenBlacklistService.BlacklistToken(token);
        return Ok(new { message = "Logged out successfully" });
    }
}