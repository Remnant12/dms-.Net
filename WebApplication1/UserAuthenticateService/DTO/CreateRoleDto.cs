using System.ComponentModel.DataAnnotations;
using WebApplication1.UserService.Models;


namespace DefaultNamespace;

public class CreateRoleDto
{
    [Required]
    [MaxLength(50)]
    public string RoleName { get; set; }
}