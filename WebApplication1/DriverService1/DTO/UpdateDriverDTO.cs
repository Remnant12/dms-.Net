namespace DriverService1.DTO;

public class UpdateDriverDTO
{
    
    public int DriverId { get; set; } 
    public int UserId { get; set; } 
    public string LicenseNumber { get; set; }
    public DateTime LicenseExpirationDate { get; set; }
    public int YearsOfExperience { get; set; }
    public DateTime DateOfHire { get; set; }
    public string ProfilePhoto { get; set; }
    public string EmergencyContact { get; set; }
    public Boolean AvailabilityStatus { get; set; }
    public TimeSpan ShiftStartTime { get; set; }
    public TimeSpan ShiftEndTime { get; set; }
}