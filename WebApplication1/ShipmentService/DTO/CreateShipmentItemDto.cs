namespace ShipmentService.DTO;

public class CreateShipmentItemDto
{
    public string ProductName { get; set; }
    public string ItemType { get; set; }
    public double Weight { get; set; }
    public double Volume { get; set; }
    public double Charge { get; set; }
}