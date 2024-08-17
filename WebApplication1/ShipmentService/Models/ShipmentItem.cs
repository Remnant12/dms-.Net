using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipmentService.Models;

public class ShipmentItem
{
    [Key]
    public int ShipmentItemId { get; set; }

    [ForeignKey("Shipment")]
    public int ShipmentId { get; set; }
    public Shipment Shipment { get; set; }  // Navigation property

    [Required]
    [StringLength(100)]
    public string ProductName { get; set; }

    [Required]
    public string ItemType { get; set; } 
    [Required]
    public double Weight { get; set; }  // Weight in kilograms or pounds

    [Required]
    public double Volume { get; set; }  // Volume in cubic meters or cubic feet

    [Required]
    public decimal Charge { get; set; }
}