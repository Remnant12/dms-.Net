using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using WebApplication1.UserService.Models;

namespace UserAuthenticateService.Controller;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly UserDbContext _context;

    public RoleController(UserDbContext context)
    {
        _context = context; 
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Role>> GetRoles()
    {
        return _context.Roles.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Role> GetRole(int id)
    {
        var role = _context.Roles.Find(id);
        if (role == null)
        {
            return NotFound();
        }

        return role; 
    }

    [HttpPost]
    public async Task<ActionResult<Role>> PostRole([FromBody] CreateRoleDto createRoleDto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }
        
        var existingRole = await _context.Roles
            .FirstOrDefaultAsync(r => r.RoleName == createRoleDto.RoleName);

        if (existingRole != null)
        {
            return BadRequest("A role with this name already exists.");
        }
        
        var newRole = new Role
        {
            RoleName = createRoleDto.RoleName
        };
        
        _context.Roles.Add(newRole);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetRole), new { id = newRole.RoleId}, newRole);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRole(int id, [FromBody] CreateRoleDto updateRoleDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var existingRole = await _context.Roles.FindAsync(id);
        if (existingRole == null)
        {
            return NotFound();
        }

        existingRole.RoleName = updateRoleDto.RoleName;

        _context.Entry(existingRole).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RoleExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok(new { id = existingRole.RoleId, name = existingRole.RoleName });
        

    }
    
    private bool RoleExists(int id)
    {
        return _context.Roles.Any(e => e.RoleId   
                                       == id);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Role>> DeleteRole(int id)
    {
        var role = await 
            _context.Roles.FindAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();

        return role;
    }

}