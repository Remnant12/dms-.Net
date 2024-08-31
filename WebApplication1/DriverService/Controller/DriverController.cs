using Driver.dbConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Driver.Controller;

    
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly DriverDbContext _context;

        public DriverController(DriverDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverService.Models.Driver>>> GetDrivers()
        {
            return await _context.Drivers.ToListAsync();
        }
        
        // GET: api/Driver/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverService.Models.Driver>> GetDriver(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            return driver;
        }
        
        
        // POST: api/Driver
        [HttpPost]
        public async Task<ActionResult<DriverService.Models.Driver>> PostDriver(DriverService.Models.Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriver", new { id = driver.DriverId }, driver);
        }
        
        // PUT: api/Driver/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriver(int id, DriverService.Models.Driver driver)
        {
            if (id != driver.DriverId)
            {
                return BadRequest();
            }

            _context.Entry(driver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(id))
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
        
        // DELETE: api/Driver/5
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

        
        
        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(e => e.DriverId == id);
        }
        

    }


