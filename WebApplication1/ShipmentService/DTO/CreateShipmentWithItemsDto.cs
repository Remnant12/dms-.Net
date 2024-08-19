namespace ShipmentService.DTO;

public class CreateShipmentWithItemsDto
{
    public string RecipientName { get; set; }
    public string RecipientEmail { get; set; }
    public string RecipientPhone { get; set; }
    public string SenderAddress { get; set; }
    public DateTime SendingDate { get; set; }
    public string RecipientAddress { get; set; }
    public DateTime ReceivingDate { get; set; }
    public string ShipmentStatus { get; set; }
    public float OverallVolume { get; set; }
    public float OverallWeight { get; set; }
    public float OverallCharge { get; set; }
    
    public string Distance { get; set; }

    public List<CreateShipmentItemDto> ShipmentItems { get; set; }
}