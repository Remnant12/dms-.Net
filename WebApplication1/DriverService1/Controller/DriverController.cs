using System.Collections.Concurrent;
using DriverService1.dbConfig;
using DriverService1.DTO;
using DriverService1.Models;
using DriverService1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriverService1.Controller;

[Route("api/[controller]")]
[ApiController]
public class DriverController : ControllerBase
{
    private readonly DriverDbContext _context;
    private readonly SharedStateService _sharedStateService;

    public DriverController(DriverDbContext context, SharedStateService sharedStateService)
    {
        _context = context;
        _sharedStateService = sharedStateService;
    }
    
    [HttpGet("GetDrivers")]
    public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
    {
        return await _context.Drivers.ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Driver>> GetDriverById(int id)
    {
        var customer = await _context.Drivers.FindAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        return customer;
    }
    
    [HttpPost]
    public async Task<ActionResult<Driver>> PostDriver([FromBody] CreateDriverDTO driverCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        Console.WriteLine($"SharedStateService.UserId in Controller: {_sharedStateService.UserId}");
        Console.WriteLine($"UserId in driverCreateDto: {driverCreateDto.UserId}");
        
        // Assign UserId from SharedStateService if it's not already set in the DTO
        if (driverCreateDto.UserId == 0 && _sharedStateService.UserId.HasValue)
        {
            driverCreateDto.UserId = _sharedStateService.UserId.Value;
        }
        
        if (!_sharedStateService.UserId.HasValue || _sharedStateService.UserId != driverCreateDto.UserId)
        {
            return BadRequest("UserId is not valid.");
        }

        var driver = new Driver
        {
            UserId = driverCreateDto.UserId,
            LicenseNumber = driverCreateDto.LicenseNumber,
            LicenseExpirationDate = driverCreateDto.LicenseExpirationDate,
            YearsOfExperience = driverCreateDto.YearsOfExperience,
            DateOfHire = driverCreateDto.DateOfHire,
            ProfilePhoto = driverCreateDto.ProfilePhoto,
            EmergencyContact = driverCreateDto.EmergencyContact,
            AvailabilityStatus = driverCreateDto.AvailabilityStatus,
            ShiftStartTime = driverCreateDto.ShiftStartTime,
            ShiftEndTime = driverCreateDto.ShiftEndTime
        };

        _context.Drivers.Add(driver);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDriverById), new { id = driver.DriverId }, driver);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDriver(int id, [FromBody] UpdateDriverDTO driverUpdateDto)
    {
        if (id != driverUpdateDto.DriverId)
        {
            return BadRequest();
        }

        var driver = await _context.Drivers.FindAsync(id);

        if (driver == null)
        {
            return NotFound();
        }

        // Update the driver properties
        driver.UserId = driverUpdateDto.UserId;
        driver.LicenseNumber = driverUpdateDto.LicenseNumber;
        driver.LicenseExpirationDate = driverUpdateDto.LicenseExpirationDate;
        driver.YearsOfExperience = driverUpdateDto.YearsOfExperience;
        driver.DateOfHire = driverUpdateDto.DateOfHire;
        driver.ProfilePhoto = driverUpdateDto.ProfilePhoto;
        driver.EmergencyContact = driverUpdateDto.EmergencyContact;
        driver.AvailabilityStatus = driverUpdateDto.AvailabilityStatus;
        driver.ShiftStartTime = driverUpdateDto.ShiftStartTime;
        driver.ShiftEndTime = driverUpdateDto.ShiftEndTime;

        _context.Entry(driver).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(driver); 
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDriver(int id)
    {
        var driver = await _context.Drivers.FindAsync(id);

        if (driver == null)
        {
            return NotFound();
        }

        _context.Drivers.Remove(driver);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}