using Microsoft.AspNetCore.Mvc;
using WebApplication1.dbConfig;
using WebApplication1.Models;

namespace WebApplication1.Controller;

[Route("api/[controller]")]
[ApiController]
public class DriverController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public DriverController(AppDbContext context)
    {
        _dbContext = context;
    }
    
    [HttpGet("/drivers")]
    public ActionResult<IEnumerable<Driver>> GetDrivers()
    {
        
        var drivers = _dbContext.Drivers.ToList();
        return Ok(drivers);
    }
    
    [HttpGet("/drivers/{id}")]
    public ActionResult<Driver> GetDriverById(int id)
    {
        var driver = _dbContext.Drivers.Find(id);

        if (driver == null)
        {
            return NotFound();
        }

        return Ok(driver);
    }
    
    [HttpPost("/drivers")]
    public ActionResult<Driver> PostDriver(Driver driver)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _dbContext.Drivers.Add(driver);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetDrivers), new { id = driver.Id }, driver);
    }
    
    [HttpPut("/drivers/{id}")]
    public IActionResult PutDriver(int id, Driver driver)
    {
        if (id != driver.Id)
        {
            return BadRequest("Driver ID in URL does not match driver ID in body");
        }

        var existingDriver = _dbContext.Drivers.Find(id);
        if (existingDriver == null)
        {
            return NotFound();
        }

        _dbContext.Entry(existingDriver).CurrentValues.SetValues(driver);
        _dbContext.SaveChanges();

        return NoContent();
    }
    
    
    [HttpDelete("/drivers/{id}")]
    public IActionResult DeleteDriver(int id)
    {
        var driver = _dbContext.Drivers.Find(id);
        if (driver == null)
        {
            return NotFound();
        }

        _dbContext.Drivers.Remove(driver);
        _dbContext.SaveChanges();

        return NoContent();
    }
}