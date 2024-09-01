using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DefaultNamespace;
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

    public UsersController(IConfiguration configuration, UserDbContext context)
    {
        _configuration = configuration;  
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
    
    [HttpPost]
    public async Task<ActionResult<User>> PostUser([FromBody] CreateUserDto userDto)
    {
        // user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        // _context.Users.Add(user);
        // await _context.SaveChangesAsync();
        // return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        // Check if the provided RoleId exists in the Roles table
        var roleExists = await _context.Roles.AnyAsync(r => r.RoleId == userDto.RoleId);
        if (!roleExists)
        {
            // If RoleId doesn't exist, throw an exception or return a bad request
            return BadRequest($"RoleId {userDto.RoleId} is invalid. Please provide a valid RoleId.");
        }
        
        var user = new User
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Address = userDto.Address,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            Country = userDto.Country,
            PostalCode = userDto.PostalCode,
            Province = userDto.Province,
            City = userDto.City,
            Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            RoleId = userDto.RoleId 
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }


    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users
            .Include(u => u.Role) // This will ensure the Role is included
            .FirstOrDefaultAsync(u => u.Id == id);
        
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
    public async Task<IActionResult> PutUser(int id, [FromBody] CreateUserDto updateUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var existingUser = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
        
        if (existingUser == null)
        {
            return NotFound();
        }
        
        existingUser.FirstName = updateUserDto.FirstName;
        existingUser.LastName = updateUserDto.LastName;
        existingUser.Address = updateUserDto.Address;
        existingUser.Email = updateUserDto.Email;
        existingUser.PhoneNumber = updateUserDto.PhoneNumber;
        existingUser.Country = updateUserDto.Country;
        existingUser.PostalCode = updateUserDto.PostalCode;
        existingUser.Province = updateUserDto.Province;
        existingUser.City = updateUserDto.City;
        
        // if (updateUserDto.RoleIds != null && updateUserDto.RoleIds.Any())
        // {
        //     // Remove existing roles
        //     _context.UserRoles.RemoveRange(existingUser.UserRoles);
        //
        //     // Assign new roles
        //     var userRoles = updateUserDto.RoleIds.Select(roleId => new UserRole
        //     {
        //         UserId = existingUser.Id,
        //         RoleId = roleId
        //     }).ToList();
        //
        //     _context.UserRoles.AddRange(userRoles);
        // }
        
        // Fetch the role from the database
        var role = await _context.Roles.FindAsync(updateUserDto.RoleId);
        if (role == null)
        {
            return BadRequest("Invalid role ID");
        }
        
        // Update the role ID for the user
        existingUser.RoleId = updateUserDto.RoleId;
        
        _context.Entry(existingUser).State = EntityState.Modified;

        
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
        
        return Ok(new 
        {
            id = existingUser.Id, 
            firstName = existingUser.FirstName,
            lastName = existingUser.LastName,
            email = existingUser.Email,
            roleIds = updateUserDto.RoleId
        });

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
}
