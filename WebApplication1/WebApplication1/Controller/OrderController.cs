// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using WebApplication1.dbConfig;
// using WebApplication1.Models;
//
// namespace WebApplication1.Controller;
//
//
// [Route("[controller]")]
// [ApiController]
// public class OrderController : ControllerBase
// {
//     private readonly AppDbContext _dbContext;
//     
//     public OrderController(AppDbContext dbContext)
//     {
//         this._dbContext = dbContext;
//     }
//     
//     [HttpGet]
//     public async Task<ActionResult<IEnumerable<object>>> GetOrders()
//     {
//         var orders = await _dbContext.Orders
//             .Include(o => o.Items)
//             .Include(o => o.Driver) // Include the Driver entity
//             .Select(o => new
//             {
//                 o.Id,
//                 o.firstName,
//                 o.lastName,
//                 o.PhoneNo,
//                 o.Email,
//                 o.PickUpAddress,
//                 o.PickUpTime,
//                 o.DeliveryAddress,
//                 o.DeliveryTime,
//                 Items = o.Items.Select(i => new 
//                 {
//                     i.Id,
//                     i.ItemName,
//                     i.ItemType,
//                     i.Weight,
//                     i.Volume,
//                     i.Total
//                 }).ToList(),
//                 o.OrderStatus,
//                 o.Distance,
//                 o.overallWeight,
//                 o.overallVolume,
//                 o.overallCharge,
//                 o.PaymentStatus,
//                 o.UserId,
//                 o.DriverId,
//                 DriverName = o.Driver.Name // Add DriverName to the result
//             })
//             .ToListAsync();
//
//         return Ok(orders);
//     }
//
//     
//     [HttpGet("{id}")]
//     public async Task<ActionResult<Order>> GetOrderById(int id)
//     {
//         var order = await _dbContext.Orders
//             .Include(o => o.Items)
//             .FirstOrDefaultAsync(o => o.Id == id);
//
//         if (order == null)
//         {
//             return NotFound();
//         }
//
//         return order;
//     }
//     
//     [HttpPost("/orders")]
//     public async Task<ActionResult<Order>> PostOrder(Order order)
//     {
//         if (!ModelState.IsValid)
//         {
//             return BadRequest(ModelState);
//         }
//
//         // Optionally, you can validate if the user exists
//         // var user = _dbContext.Users.Find(order.UserId);
//         if (user == null)
//         {
//             return NotFound("User not found");
//         }
//         
//         var driver = _dbContext.Drivers.Find(order.DriverId);
//         if (driver == null)
//         {
//             return NotFound("Driver not Found. Required!"); 
//         }
//
//         _dbContext.Orders.Add(order);
//         _dbContext.SaveChanges();
//
//         return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order);
//     }
//     
//     
//     [HttpDelete("{id}")]
//     public async Task<IActionResult> DeleteOrder(int id)
//     {
//         var order = await _dbContext.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
//         
//         if (order == null)
//         {
//             return NotFound();
//         }
//
//         _dbContext.Orders.Remove(order);
//         await _dbContext.SaveChangesAsync();
//
//         return NoContent();
//     }
//     
//     [HttpPut("/orders/{id}")]
//     public async Task<IActionResult> PutOrder(int id, Order updatedOrder)
//     {
//         if (id != updatedOrder.Id)
//         {
//             return BadRequest("Order ID in URL does not match order ID in body");
//         }
//
//         if (!ModelState.IsValid)
//         {
//             return BadRequest(ModelState);
//         }
//
//         // Optionally, you can validate if the user exists
//         var user = await _dbContext.Users.FindAsync(updatedOrder.UserId);
//         if (user == null)
//         {
//             return NotFound("User not found");
//         }
//     
//         // Validate if the driver exists
//         var driver = await _dbContext.Drivers.FindAsync(updatedOrder.DriverId);
//         if (driver == null)
//         {
//             return NotFound("Driver not found");
//         }
//
//         var existingOrder = await _dbContext.Orders.FindAsync(id);
//         if (existingOrder == null)
//         {
//             return NotFound("Order not found");
//         }
//
//         // Update the existing order entity with values from updatedOrder
//         _dbContext.Entry(existingOrder).CurrentValues.SetValues(updatedOrder);
//
//         try
//         {
//             await _dbContext.SaveChangesAsync();
//         }
//         catch (DbUpdateConcurrencyException)
//         {
//             if (!OrderExists(id))
//             {
//                 return NotFound("Order not found");
//             }
//             else
//             {
//                 throw;
//             }
//         }
//
//         return NoContent();
//     }
//
//     private bool OrderExists(int id)
//     {
//         return _dbContext.Orders.Any(e => e.Id == id);
//     }
//     
// }