using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserService.Data;
using WebApplication1.UserService.Models;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserDbContext _context;
    
    public LoginController(IConfiguration configuration, UserDbContext context)
    {
        _configuration = configuration;
        _context = context;

    }


    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] Login login)
    {
        var user = Authenticate(login);
        if (user != null)
        {
            var token = Generate(user);
            return Ok(new { token });
        }

        return NotFound("User Not Found");
    }

    private User Authenticate(Login login)
    {
        // Replace with your actual authentication logic.
        var user = _context.Users.SingleOrDefault(u => u.Email == login.Email);

        // If user is found, return it, otherwise return null
        return user;
    }

    private string Generate(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ODo0EHIGJqjIeX8KJFCgZRY5Jyq46eiyqYCCg8kkcKYqssg4bzNh62AHDAfdIQf"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.Password),
            
        };

        var token = new JwtSecurityToken(
            // issuer: "http://localhost:5001",
            // audience: "http://localhost:5001",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}