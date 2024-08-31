using System.ComponentModel.DataAnnotations;

namespace DriverService1.Models;

public class Driver
{
    [Key]
    public int DriverId { get; set; } // Primary Key: Unique identifier for each driver

    public int UserId { get; set; }

    [Required]
    [StringLength(500)]
    public string LicenseNumber { get; set; }
    
    [Required]
    public DateTime LicenseExpirationDate { get; set; }

    [Required]
    public int YearsOfExperience { get; set; }

    [Required]
    public DateTime DateOfHire { get; set; }

    [StringLength(500)]
    public string ProfilePhoto { get; set; } // URL or binary data for profile picture

    [StringLength(200)]
    public string EmergencyContact { get; set; } // Emergency contact details

    [Required]
    [StringLength(50)]
    public Boolean AvailabilityStatus { get; set; } // e.g., On Duty, Off Duty

    [Required]
    public TimeSpan ShiftStartTime { get; set; } // Start time of the current or next shift
    
    [Required]
    public TimeSpan ShiftEndTime { get; set; } // End time of the current or next shift
}