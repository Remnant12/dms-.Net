using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.dbConfig;
using WebApplication1.Models;

namespace WebApplication1.Controller;


[Route("[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    
    public OrderController(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        // return await _dbContext.Orders.ToListAsync();
        return await _dbContext.Orders
            .Include(o => o.Items)
            .ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderById(int id)
    {
        var order = await _dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        return order;
    }
    
    [HttpPost("/orders")]
    public async Task<ActionResult<Order>> PostOrder(Order orderDto)
    {
         if (!ModelState.IsValid) {
            return BadRequest(ModelState); 
         }

         
        var order = new Order 
        {
            firstName = orderDto.firstName,
            lastName = orderDto.lastName,
            PhoneNo = orderDto.PhoneNo,
            Email = orderDto.Email,
            PickUpAddress = orderDto.PickUpAddress,
            PickUpTime = orderDto.PickUpTime,
            DeliveryAddress = orderDto.DeliveryAddress,
            DeliveryTime = orderDto.DeliveryTime,
            OrderStatus = orderDto.OrderStatus,
            Driver = orderDto.Driver,
            overallWeight = orderDto.overallWeight, 
            overallCharge = orderDto.overallCharge, 
            overallVolume = orderDto.overallVolume, 
            PaymentStatus = orderDto.PaymentStatus,
            Distance = orderDto.Distance,
            Items = orderDto.Items.Select(i => new Item
            {
                ItemName = i.ItemName,
                ItemType = i.ItemType,  
                Weight = i.Weight,
                Volume = i.Volume,
                Total = i.Total
            }).ToList() 
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order);
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _dbContext.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
        
        if (order == null)
        {
            return NotFound();
        }

        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, Order orderDto)
    {
        // Retrieve the existing order from the database
        var existingOrder = await _dbContext.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
    
        if (existingOrder == null)
        {
            return NotFound(); // Return a 404 if the order is not found
        }

        // Update the existing order's properties with the values from orderDto
        existingOrder.firstName = orderDto.firstName;
        existingOrder.lastName = orderDto.lastName;
        existingOrder.PhoneNo = orderDto.PhoneNo;
        existingOrder.Email = orderDto.Email;
        existingOrder.PickUpAddress = orderDto.PickUpAddress;
        existingOrder.PickUpTime = orderDto.PickUpTime;
        existingOrder.DeliveryAddress = orderDto.DeliveryAddress;
        existingOrder.DeliveryTime = orderDto.DeliveryTime;
        existingOrder.OrderStatus = orderDto.OrderStatus;
        existingOrder.Driver = orderDto.Driver;
        existingOrder.Distance = orderDto.Distance;
        existingOrder.overallWeight = orderDto.overallWeight;
        existingOrder.overallCharge = orderDto.overallCharge;
        existingOrder.overallVolume = orderDto.overallVolume;
        existingOrder.PaymentStatus = orderDto.PaymentStatus;

        // Clear existing items and add updated items
        existingOrder.Items.Clear();
        existingOrder.Items = orderDto.Items.Select(i => new Item
        {
            ItemName = i.ItemName,
            ItemType = i.ItemType,
            Weight = i.Weight,
            Volume = i.Volume,
            Total = i.Total
        }).ToList();

        // Save changes to the database
        await _dbContext.SaveChangesAsync();

        return NoContent(); // Return a 204 No Content response to indicate successful update
    }

    
    
    
}