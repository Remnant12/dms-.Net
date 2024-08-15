using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentService.dbConfig;
using ShipmentService.DTO;
using ShipmentService.Models;

namespace ShipmentService.Controller;

[Route("api/[controller]")]
[ApiController]
public class ShipmentController : ControllerBase
{
    private readonly ShipmentDbContext _context;
    
    public ShipmentController(ShipmentDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Shipment>>> GetShipments()
    {
        return await _context.Shipments.Include(s => s.ShipmentItems).ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Shipment>> GetShipmentById(int id)
    {
        var shipment = await _context.Shipments
            .Include(s => s.ShipmentItems)
            .FirstOrDefaultAsync(s => s.ShipmentId == id);

        if (shipment == null)
        {
            return NotFound();
        }

        return shipment;
    }
    
    [HttpPost]
    public async Task<ActionResult<Shipment>> PostShipment([FromBody] CreateShipmentWithItemsDto shipmentWithItemsDto)
    {
        // _context.Shipments.Add(shipment);
        // await _context.SaveChangesAsync();
        //
        // return CreatedAtAction(nameof(GetShipment), new { id = shipment.ShipmentId }, shipment);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        var shipment = new Shipment
        {
            CustomerId = shipmentWithItemsDto.CustomerId,
            RecipientName = shipmentWithItemsDto.RecipientName,
            RecipientEmail = shipmentWithItemsDto.RecipientEmail,
            RecipientPhone = shipmentWithItemsDto.RecipientPhone,
            SenderAddress = shipmentWithItemsDto.SenderAddress,
            SendingDate = shipmentWithItemsDto.SendingDate,
            RecipientAddress = shipmentWithItemsDto.RecipientAddress,
            ReceivingDate = shipmentWithItemsDto.ReceivingDate,
            ShipmentStatus = shipmentWithItemsDto.ShipmentStatus,
            TrackingNumber = shipmentWithItemsDto.TrackingNumber,
            DriverId = shipmentWithItemsDto.DriverId
        }; 
        
        _context.Shipments.Add(shipment);
        await _context.SaveChangesAsync();

        var shipmentItems = shipmentWithItemsDto.ShipmentItems.Select(itemDto => new ShipmentItem
        {
            ShipmentId = shipment.ShipmentId, // Use the ID of the created shipment
            ProductName = itemDto.ProductName,
            Distance = itemDto.Distance,
            OverallWeight = itemDto.OverallWeight,
            OverallVolume = itemDto.OverallVolume,
            OverallCharge = (decimal)itemDto.OverallCharge 
        }).ToList();
        
        _context.ShipmentItems.AddRange(shipmentItems);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetShipmentById), new { id = shipment.ShipmentId }, shipment);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutShipment(int id, Shipment shipment)
    {
        if (id != shipment.ShipmentId)
        {
            return BadRequest();
        }

        _context.Entry(shipment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShipmentExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
    
    private bool ShipmentExists(int id)
    {
        return _context.Shipments.Any(e => e.ShipmentId == id);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShipment(int id)
    {
        var shipment = await _context.Shipments.FindAsync(id);
        if (shipment == null)
        {
            return NotFound();
        }

        _context.Shipments.Remove(shipment);
        await _context.SaveChangesAsync();

        return NoContent();
    }


}