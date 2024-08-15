namespace ShipmentService.DTO;

public class CreateShipmentItemDto
{
    public string ProductName { get; set; }
    public double Distance { get; set; }
    public double OverallWeight { get; set; }
    public double OverallVolume { get; set; }
    public double OverallCharge { get; set; }
}