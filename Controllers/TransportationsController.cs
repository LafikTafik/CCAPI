using Microsoft.AspNetCore.Mvc;
using CCAPI.Models;
using CCAPI.DTO;
using Microsoft.EntityFrameworkCore;

namespace CCAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransportationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransportationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/transportations
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transportations = await _context.Transportations.ToListAsync();
            return Ok(transportations);
        }

        // GET: api/transportations/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transportation = await _context.Transportations.FindAsync(id);

            if (transportation == null)
                return NotFound();

            return Ok(transportation);
        }

        // POST: api/transportations
        [HttpPost]
        public async Task<IActionResult> Create(TransportationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transportation = new Transportation
            {
                ActiveVehicle = dto.ActiveVehicle,
                CargoID = dto.LoadId,
                VehicleID = dto.VehicleId
            };

            _context.Transportations.Add(transportation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = transportation.ActiveVehicle }, transportation);
        }

        // PUT: api/transportations/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TransportationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _context.Transportations.FindAsync(id);

            if (existing == null)
                return NotFound();

            existing.CargoID = dto.LoadId;
            existing.VehicleID = dto.VehicleId;

            _context.Transportations.Update(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/transportations/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transportation = await _context.Transportations.FindAsync(id);

            if (transportation == null)
                return NotFound();

            _context.Transportations.Remove(transportation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}