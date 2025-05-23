using Microsoft.AspNetCore.Mvc;
using CCAPI.Models;
using CCAPI.DTO;
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
            var drivers = await _context.Drivers.ToListAsync();
            return Ok(drivers);
        }

        // GET: api/drivers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);

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
                PhoneNumber = dto.PhoneNumber
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

            var existingDriver = await _context.Drivers.FindAsync(id);

            if (existingDriver == null)
                return NotFound();

            existingDriver.FirstName = dto.FirstName;
            existingDriver.LastName = dto.LastName;
            existingDriver.LicenseNumber = dto.LicenseNumber;
            existingDriver.PhoneNumber = dto.PhoneNumber;

            _context.Drivers.Update(existingDriver);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/drivers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);

            if (driver == null)
                return NotFound();

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}