using Microsoft.AspNetCore.Mvc;
using CCAPI.Models;
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
        public async Task<IActionResult> Create(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = driver.ID }, driver);
        }

        // PUT: api/drivers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Driver driver)
        {
            if (id != driver.ID)
                return BadRequest();

            _context.Entry(driver).State = EntityState.Modified;
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