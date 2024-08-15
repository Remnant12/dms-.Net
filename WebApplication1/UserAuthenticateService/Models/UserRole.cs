using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.UserService.Models;

public class UserRole
{
    [Key]
    public int UserRoleId { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }

    [ForeignKey("Role")]
    public int RoleId { get; set; }
    public Role Role { get; set; }
}