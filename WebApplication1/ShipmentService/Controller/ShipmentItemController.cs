using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentService.dbConfig;
using ShipmentService.Models;
using ShipmentService.Services.Interface;

namespace ShipmentService.Controller;

[Route("api/[controller]")]
[ApiController]
public class ShipmentItemController : ControllerBase
{
    private readonly ShipmentDbContext _context;
    private readonly ITrackingNumberGenerator _trackingNumberGenerator;

    public ShipmentItemController(ShipmentDbContext context, ITrackingNumberGenerator trackingNumberGenerator)
    {
        _context = context;
        _trackingNumberGenerator = trackingNumberGenerator;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipmentItem>>> GetShipmentItems()
    {
        return await _context.ShipmentItems.ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ShipmentItem>> GetShipmentItem(int id)
    {
        var shipmentItem = await _context.ShipmentItems.FindAsync(id);

        if (shipmentItem == null)
        {
            return NotFound();
        }

        return shipmentItem;
    }
    
    [HttpPost]
    public async Task<ActionResult<ShipmentItem>> PostShipmentItem(ShipmentItem shipmentItem)
    {
        _context.ShipmentItems.Add(shipmentItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetShipmentItem), new { id = shipmentItem.ShipmentItemId }, shipmentItem);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutShipment(int id, [FromBody] Shipment shipment)
    {
        if (id != shipment.ShipmentId)
        {
            return BadRequest();
        }

        // Attach the entity and mark it as modified
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

        // Fetch the updated entity to return
        var updatedShipment = await _context.Shipments.FindAsync(id);

        if (updatedShipment == null)
        {
            return NotFound();
        }

        return Ok(updatedShipment);
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
    
    private bool ShipmentExists(int id)
    {
        return _context.Shipments.Any(e => e.ShipmentId == id);
    }
}