using Microsoft.AspNetCore.Mvc;
using CCAPI.Models;
using CCAPI.DTO.defaultt;
using CCAPI.DTO.deleted;
using Microsoft.EntityFrameworkCore;

namespace CCAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DriversController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/drivers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var drivers = await _context.Drivers
                .Where(d => !d.IsDeleted)
                .Select(d => new DriverDto
                {
                    ID = d.ID,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    LicenseNumber = d.LicenseNumber,
                    PhoneNumber = d.PhoneNumber
                })
                .ToListAsync();

            return Ok(drivers);
        }

        // GET: api/drivers/deleted
        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeleted()
        {
            var deletedDrivers = await _context.Drivers
                .Where(d => d.IsDeleted)
                .Select(d => new DeletedDriverDto
                {
                    ID = d.ID,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    LicenseNumber = d.LicenseNumber,
                    PhoneNumber = d.PhoneNumber,

                    IsDeleted = d.IsDeleted,
                    DeletedAt = d.DeletedAt
                })
                .ToListAsync();

            return Ok(deletedDrivers);
        }

        // GET: api/drivers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var driver = await _context.Drivers
                .Where(d => !d.IsDeleted && d.ID == id)
                .Select(d => new DriverDto
                {
                    ID = d.ID,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    LicenseNumber = d.LicenseNumber,
                    PhoneNumber = d.PhoneNumber
                })
                .FirstOrDefaultAsync();

            if (driver == null)
                return NotFound();

            return Ok(driver);
        }

        // POST: api/drivers
        [HttpPost]
        public async Task<IActionResult> Create(DriverDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var driver = new Driver
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                LicenseNumber = dto.LicenseNumber,
                PhoneNumber = dto.PhoneNumber,
                IsDeleted = false,
                DeletedAt = null
            };

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = driver.ID }, driver);
        }

        // PUT: api/drivers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DriverDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _context.Drivers.FindAsync(id);

            if (existing == null || existing.IsDeleted)
                return NotFound();

            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.LicenseNumber = dto.LicenseNumber;
            existing.PhoneNumber = dto.PhoneNumber;

            _context.Drivers.Update(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/drivers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);

            if (driver == null || driver.IsDeleted)
                return NotFound();

            driver.IsDeleted = true;
            driver.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/drivers/restore/{id}
        [HttpPost("restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);

            if (driver == null)
                return NotFound();

            if (!driver.IsDeleted)
                return BadRequest("Водитель не удален");

            driver.IsDeleted = false;
            driver.DeletedAt = null;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}