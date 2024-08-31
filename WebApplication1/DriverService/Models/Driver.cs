using System.ComponentModel.DataAnnotations;

namespace DriverService.Models;

public class Driver
{
    [Key]
    public int DriverId { get; set; } // Primary Key: Unique identifier for each driver

    public int UserId { get; set; }

    [Required]
    public DateTime LicenseNumber { get; set; }
    
    [Required]
    public DateTime LicenseExpirationDate { get; set; }

    [Required]
    public int YearsOfExperience { get; set; }

    [Required]
    public DateTime DateOfHire { get; set; }

    [Required]
    [StringLength(500)]
    public string ProfilePhoto { get; set; } // URL or binary data for profile picture

    [Required]
    [StringLength(200)]
    public string EmergencyContact { get; set; } // Emergency contact details

    [Required]
    [StringLength(50)]
    public string AvailabilityStatus { get; set; } // e.g., On Duty, Off Duty

    [Required]
    public DateTime ShiftStartTime { get; set; } // Start time of the current or next shift
    
    [Required]
    public DateTime ShiftEndTime { get; set; } // End time of the current or next shift

}