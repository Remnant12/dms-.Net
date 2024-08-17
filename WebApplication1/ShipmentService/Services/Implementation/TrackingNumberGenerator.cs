using ShipmentService.Services.Interface;

public class TrackingNumberGenerator : ITrackingNumberGenerator
{
    public string GenerateTrackingNumber()
    {
        // Generate a unique tracking number using timestamp and random number
        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        var randomNumber = new Random().Next(1000, 9999);
        return $"TRACK-{timestamp}-{randomNumber}";
    }
}