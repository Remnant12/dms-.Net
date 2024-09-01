using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.UserService.Models;

public class Role
{
    [Key]
    public int RoleId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string RoleName { get; set; }

    // [JsonIgnore]
    // public ICollection<UserRole> UserRoles { get; set; }
    
    [Required]
    public ICollection<User> Users { get; set; }
}