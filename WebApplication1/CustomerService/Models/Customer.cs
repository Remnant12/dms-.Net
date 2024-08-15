using System.ComponentModel.DataAnnotations;

namespace CustomerService.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }  // Primary key

    [Required]
    [StringLength(100)]
    public string FullName { get; set; }  // Full name of the customer

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; }  // Email address

    [Required]
    [Phone]
    [StringLength(15)]
    public string Phone { get; set; }  // Phone number

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}