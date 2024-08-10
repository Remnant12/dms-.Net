using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserService.Data;
using WebApplication1.UserService.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserDbContext _context;

    public UsersController(UserDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
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
    [Authorize]
    public IActionResult SecureEndpoint()
    {
        var token = Request.Headers["Authorization"].ToString();
        Console.WriteLine("Received Token: " + token);
        return Ok("Access granted");
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
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
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("A345fghJkl987654321QWERTYUIOPasdfsdfghjklzxcvbnm1234567890");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var tokenString = tokenHandler.WriteToken(token);

        var parts = tokenString.Split('.');
        if (parts.Length == 3)
        {
            Console.WriteLine("Token format is valid.");
        }
        else
        {
            Console.WriteLine("Token format is invalid.");
        }
        
        return tokenString;

        
        // var token = tokenHandler.CreateToken(tokenDescriptor);
        // return tokenHandler.WriteToken(token);
        
    }
    
}
