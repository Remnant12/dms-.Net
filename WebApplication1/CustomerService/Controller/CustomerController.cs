using CustomerService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Controller;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomerDbContext _context;
    
    public CustomerController(CustomerDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        return await _context.Customers.ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        return customer;
    }
    
    [HttpPost]
    public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
    {
        // Hash password (consider using a more secure approach)
        customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customer);
    }
    
    // PUT: api/Customer/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, Customer customer)
    {
        if (id != customer.CustomerId)
        {
            return BadRequest();
        }

        // Optionally, hash the password if it has been updated
        // customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CustomerExists(id))
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
    
    // DELETE: api/Customer/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool CustomerExists(int id)
    {
        return _context.Customers.Any(e => e.CustomerId == id);
    }

}