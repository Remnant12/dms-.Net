using System.ComponentModel.DataAnnotations;


namespace DefaultNamespace;

public class CreateRoleDto
{
    [Required]
    [MaxLength(50)]
    public string RoleName { get; set; }
}