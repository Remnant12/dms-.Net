namespace ShipmentService.DTO;

public class CreateShipmentWithItemsDto
{
    public int CustomerId { get; set; }
    public string RecipientName { get; set; }
    public string RecipientEmail { get; set; }
    public string RecipientPhone { get; set; }
    public string SenderAddress { get; set; }
    public DateTime SendingDate { get; set; }
    public string RecipientAddress { get; set; }
    public DateTime ReceivingDate { get; set; }
    public string ShipmentStatus { get; set; }
    public string OverallVolume { get; set; }
    public string OverallWeight { get; set; }
    public string OverallCharge { get; set; }
    
    public string Distance { get; set; }

    public string TrackingNumber { get; set; }
    public int DriverId { get; set; }
    public List<CreateShipmentItemDto> ShipmentItems { get; set; }
}