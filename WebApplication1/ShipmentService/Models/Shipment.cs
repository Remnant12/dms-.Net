using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShipmentService.Constants;

namespace ShipmentService.Models;

public class Shipment
{
    [Key]
    public int ShipmentId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(100)]
    public string RecipientName { get; set; }

    [Required]
    [StringLength(200)]
    [EmailAddress]
    public string RecipientEmail { get; set; }

    [Required]
    [StringLength(15)]
    [Phone]
    public string RecipientPhone { get; set; }

    // Sender's address (selected by customer)
    [Required]
    [StringLength(200)]
    public string SenderAddress { get; set; }

    // Sending date (either auto-set by the system or selected by the customer)
    [Required]  // Or make it nullable if the system sets it later
    public DateTime SendingDate { get; set; }

    [Required]
    [StringLength(200)]
    public string RecipientAddress { get; set; }

    public DateTime? ReceivingDate { get; set; }

    [Required]
    [StringLength(50)]
    public string ShipmentStatus { get; set; }
    [Required]
    [StringLength(50)]
    public float OverallVolume { get; set; }
    [Required]
    [StringLength(50)]
    public float OverallWeight { get; set; }
    [Required]
    [StringLength(50)]
    public float OverallCharge { get; set; }
    [Required]
    [StringLength(50)]
    public string Distance { get; set; }

    [Required]
    [StringLength(50)]
    public string TrackingNumber { get; set; }

    public int DriverId { get; set; }
    
    [Required]
    public PriorityLevel Priority { get; set; }
    
    
    [Required]
    public PriorityLevel Priority2 { get; set; }
    
    [Required]
    public PriorityLevel Priority3 { get; set; }
    
    public ICollection<ShipmentItem> ShipmentItems { get; set; }
}