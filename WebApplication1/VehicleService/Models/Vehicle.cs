using System.ComponentModel.DataAnnotations;

namespace VehicleService.Models;

public class Vehicle
{
    [Key]
    public int VehicleID { get; set; } // Primary Key: Unique identifier for each vehicle

    [Required]
    [StringLength(20)]
    public string LicensePlate { get; set; } // The vehicle’s license plate number

    [Required]
    [StringLength(50)]
    public string Make { get; set; } // The manufacturer of the vehicle

    [Required]
    [StringLength(50)]
    public string Model { get; set; } // The specific model of the vehicle

    [Required]
    public int Year { get; set; } // The year the vehicle was manufactured

    [Required]
    [StringLength(30)]
    public string Type { get; set; } // The type of vehicle

    [Required]
    public decimal Capacity { get; set; } // Maximum cargo weight the vehicle can carry

    [Required]
    public decimal Volume { get; set; } // Maximum volume of cargo the vehicle can hold

    [Required]
    [StringLength(20)]
    public string FuelType { get; set; } // Type of fuel the vehicle uses

    [Required]
    public decimal FuelEfficiency { get; set; } // Fuel efficiency in km per liter

    [Required]
    public decimal CurrentMileage { get; set; } // Current odometer reading

    // [Required]
    // [StringLength(20)]
    // public string Status { get; set; } // Current status of the vehicle
    //
    // public DateTime LastMaintenanceDate { get; set; } // Date of the last maintenance check
    //
    // public DateTime? NextMaintenanceDue { get; set; } // Date when the next maintenance is due
    //
    // [StringLength(100)]
    // public string GPSLocation { get; set; } // Last known or real-time GPS location
    //
    // [StringLength(100)]
    // public string InsuranceDetails { get; set; } // Information about the vehicle's insurance

    public DateOnly RegistrationExpiration { get; set; } // Date when the vehicle's registration expires

}