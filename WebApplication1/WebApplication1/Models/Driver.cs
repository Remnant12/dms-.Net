using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Driver
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [Phone]
    [StringLength(15)]
    public string Phone { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string VehicleType { get; set; }

    [Required]
    [StringLength(20)]
    public string LicenseNumber { get; set; }

    [Required]
    [StringLength(200)]
    public string CurrentLocation { get; set; }

    [Required]
    public bool Availability { get; set; }

    [Required]
    public DateTime ShiftStartTime { get; set; }

    [Required]
    public DateTime ShiftEndTime { get; set; }

}