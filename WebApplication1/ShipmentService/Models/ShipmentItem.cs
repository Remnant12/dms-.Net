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
    public double Distance { get; set; }  // Distance in kilometers or miles

    [Required]
    public double OverallWeight { get; set; }  // Weight in kilograms or pounds

    [Required]
    public double OverallVolume { get; set; }  // Volume in cubic meters or cubic feet

    [Required]
    public decimal OverallCharge { get; set; }
}