using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentService.dbConfig;
using ShipmentService.DTO;
using ShipmentService.Models;
using ShipmentService.Services.Implementation;
using ShipmentService.Services.Interface;

namespace ShipmentService.Controller;

[Route("api/[controller]")]
[ApiController]
public class ShipmentController : ControllerBase
{
    private readonly ShipmentDbContext _context;
    private readonly ITrackingNumberGenerator _trackingNumberGenerator;
    private readonly ILogger<ShipmentController> _logger;
    private CalculatePriority _calculatePriority;
    
    
    // private readonly ICustomerService _customerService;

    public ShipmentController(ShipmentDbContext context, ITrackingNumberGenerator? trackingNumberGenerator,
        ICustomerService? customerService, ILogger<ShipmentController> logger, CalculatePriority calculatePriority)
    {
        _context = context;
        _trackingNumberGenerator = trackingNumberGenerator;
        _logger = logger; 
        _calculatePriority = calculatePriority;
        
        // _customerService = customerService;

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
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var token = HttpContext.Request.Headers["Authorization"].ToString();
            // _logger.LogInformation("Received token: {Token}", token);
            //
            // var customerId = await GetCustomerIdFromCustomerService(token);
            //
            // if (customerId == null)
            // {
            //     return Unauthorized("Invalid token or customer not found.");
            // }

            var shipment = new Shipment
            {
                // CustomerId = shipmentWithItemsDto.CustomerId,
                // CustomerId = customerId.Value,
                CustomerId = 1,
                RecipientName = shipmentWithItemsDto.RecipientName,
                RecipientEmail = shipmentWithItemsDto.RecipientEmail,
                RecipientPhone = shipmentWithItemsDto.RecipientPhone,
                SenderAddress = shipmentWithItemsDto.SenderAddress,
                SendingDate = shipmentWithItemsDto.SendingDate,
                RecipientAddress = shipmentWithItemsDto.RecipientAddress,
                ReceivingDate = shipmentWithItemsDto.ReceivingDate,
                ShipmentStatus = shipmentWithItemsDto.ShipmentStatus,
                OverallVolume = shipmentWithItemsDto.OverallVolume,
                OverallWeight = shipmentWithItemsDto.OverallWeight,
                OverallCharge = shipmentWithItemsDto.OverallCharge,
                Distance = shipmentWithItemsDto.Distance,
                TrackingNumber = _trackingNumberGenerator.GenerateTrackingNumber(),
                // TrackingNumber = "2232",
                // DriverId = shipmentWithItemsDto.DriverId, 
                Priority = _calculatePriority.calculatePriority(shipmentWithItemsDto.SendingDate, shipmentWithItemsDto.ReceivingDate),
                DriverId = 1,
            };

            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();
            
            var shipmentItems = shipmentWithItemsDto.ShipmentItems.Select(itemDto => new ShipmentItem
            {
                ShipmentId = shipment.ShipmentId, // Use the ID of the created shipment
                ProductName = itemDto.ProductName,
                ItemType = itemDto.ItemType,
                Weight = itemDto.Weight,
                Volume = itemDto.Volume,
                Charge = (decimal)itemDto.Charge
            }).ToList();

            _context.ShipmentItems.AddRange(shipmentItems);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Shipment created successfully with ID: {ShipmentId}", shipment.ShipmentId);


            return CreatedAtAction(nameof(GetShipmentById), new { id = shipment.ShipmentId }, shipment);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating shipment: {Message}", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while creating shipment.", Details = ex.Message });
            
        }
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
    
    private async Task<int?> GetCustomerIdFromCustomerService(string token)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        _logger.LogInformation("Received token in GetCustomerIdFromCustomerService: {Token}", token);

        var response = await httpClient.GetAsync("http://localhost:5280/api/Customer/GetCustomerIdByPhone"); // Adjust URL as needed

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<int?>();
        }

        return null;
    }


}