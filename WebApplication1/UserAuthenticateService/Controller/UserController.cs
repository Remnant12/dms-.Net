using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserService.Data;
using WebApplication1.UserService.Models;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserDbContext _context;
    private readonly IConfiguration _configuration;

    public UsersController(UserDbContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpGet("secure-endpoint")]
    // [Authorize]
    public IActionResult SecureEndpoint()
    {
        // var token = Request.Headers["Authorization"].ToString();
        // Console.WriteLine("Received Token: " + token);
        return Ok("Access granted");
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
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

        var token = GenerateJwtToken(user);

        return Ok(new { Token = token });
    }

    private string GenerateJwtToken(User user)
    {
   
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ODo0EHIGJqjIeX8KJFCgZRY5Jyq46eiyqYCCg8kkcKYqssg4bzNh62AHDAfdIQf"));
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "http://localhost:5001",
            audience: "http://localhost:5001",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
