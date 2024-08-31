using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleService.dbConfig;
using VehicleService.DTO;
using VehicleService.Models;

namespace VehicleService.Controller;

[Route("api/[controller]")]
[ApiController]
public class VehicleController : ControllerBase
{
    private readonly VehicleDbContext _context;

    public VehicleController(VehicleDbContext context)
    {
        _context = context;
    }
    
    // GET: api/Vehicle
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
    {
        return await _context.Vehicles.ToListAsync();
    }
    
    // GET: api/Vehicle/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Vehicle>> GetVehicle(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);

        if (vehicle == null)
        {
            return NotFound();
        }

        return vehicle;
    }
    
    // POST: api/Vehicle
    [HttpPost]
    public async Task<ActionResult<Vehicle>> PostVehicle(CreateVehicleDTO createVehicleDto)
    {
        var vehicle = new Vehicle
        {
            LicensePlate = createVehicleDto.LicensePlate,
            Make = createVehicleDto.Make,
            Model = createVehicleDto.Model,
            Year = createVehicleDto.Year,
            Type = createVehicleDto.Type,
            Capacity = createVehicleDto.Capacity,
            Volume = createVehicleDto.Volume,
            FuelType = createVehicleDto.FuelType,
            FuelEfficiency = createVehicleDto.FuelEfficiency,
            CurrentMileage = createVehicleDto.CurrentMileage,
            // Status = createVehicleDto.Status,
            // LastMaintenanceDate = createVehicleDto.LastMaintenanceDate ?? default,
            // NextMaintenanceDue = createVehicleDto.NextMaintenanceDue,
            // GPSLocation = createVehicleDto.GPSLocation,
            // InsuranceDetails = createVehicleDto.InsuranceDetails,
            RegistrationExpiration = createVehicleDto.RegistrationExpiration
        };

        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.VehicleID }, vehicle);
    }
    
    // PUT: api/Vehicle/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVehicle(int id, UpdateVehicleDTO updateVehicleDto)
    {
        if (id != updateVehicleDto.VehicleID)
        {
            return BadRequest();
        }

        var vehicle = await _context.Vehicles.FindAsync(id);

        if (vehicle == null)
        {
            return NotFound();
        }

        vehicle.LicensePlate = updateVehicleDto.LicensePlate;
        vehicle.Make = updateVehicleDto.Make;
        vehicle.Model = updateVehicleDto.Model;
        vehicle.Year = updateVehicleDto.Year;
        vehicle.Type = updateVehicleDto.Type;
        vehicle.Capacity = updateVehicleDto.Capacity;
        vehicle.Volume = updateVehicleDto.Volume;
        vehicle.FuelType = updateVehicleDto.FuelType ;
        vehicle.FuelEfficiency = updateVehicleDto.FuelEfficiency;
        vehicle.CurrentMileage = updateVehicleDto.CurrentMileage;
        // vehicle.Status = updateVehicleDto.Status ?? vehicle.Status;
        // vehicle.LastMaintenanceDate = updateVehicleDto.LastMaintenanceDate ?? vehicle.LastMaintenanceDate;
        // vehicle.NextMaintenanceDue = updateVehicleDto.NextMaintenanceDue ?? vehicle.NextMaintenanceDue;
        // vehicle.GPSLocation = updateVehicleDto.GPSLocation ?? vehicle.GPSLocation;
        // vehicle.InsuranceDetails = updateVehicleDto.InsuranceDetails ?? vehicle.InsuranceDetails;
        vehicle.RegistrationExpiration = updateVehicleDto.RegistrationExpiration;

        _context.Entry(vehicle).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(vehicle); // Return the updated vehicle
    }

    // DELETE: api/Vehicle/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);

        if (vehicle == null)
        {
            return NotFound();
        }

        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}