using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Item
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Optionally, if using auto-increment
    public int Id { get; set; }
    public string ItemName { get; set; }
    public string ItemType { get; set; }
    public string Weight { get; set; }
    public string Volume { get; set; }
    public string Total { get; set; }
}

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string PhoneNo { get; set; }
    public string Email { get; set; }
    
    public string PickUpAddress { get; set; }
    public string PickUpTime { get; set; }
    public string DeliveryAddress { get; set; }
    public string DeliveryTime { get; set; }
    
    public List<Item> Items { get; set; }
    public string OrderStatus { get; set; }
    public string Driver { get; set; }
    
    public float Distance { get; set; }
    public float overallWeight { get; set; }
    public float overallVolume { get; set; }
    public float overallCharge { get; set; }
    public string PaymentStatus { get; set; }
}