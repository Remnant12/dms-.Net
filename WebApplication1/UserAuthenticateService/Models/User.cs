using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.UserService.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [StringLength(200)]
    public string Address { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Email { get; set; }

    [Required]
    [Phone]
    [StringLength(15)]
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(100)]
    public string Country { get; set; }

    [Required]
    [StringLength(20)]
    public string PostalCode { get; set; }

    [Required]
    [StringLength(100)]
    public string Province { get; set; }

    [Required]
    [StringLength(100)]
    public string City { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    // [JsonIgnore]
    // public ICollection<UserRole> UserRoles { get; set; }
    
    [Required]
    [ForeignKey("RoleId")]
    public int RoleId { get; set; } // Single RoleId instead of ICollection<UserRole>
    public Role Role { get; set; }
}