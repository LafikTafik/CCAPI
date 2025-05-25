using Microsoft.AspNetCore.Mvc;
using CCAPI.Models;
using CCAPI.DTO.defaultt;
using CCAPI.DTO.deleted;
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
            var transportations = await _context.Transportations
                .Where(t => !t.IsDeleted)
                .Select(t => new TransportationDto
                {
                    ActiveVehicle = t.ActiveVehicle,
                    LoadId = t.CargoID,
                    VehicleId = t.VehicleId
                })
                .ToListAsync();

            return Ok(transportations);
        }

        // GET: api/transportations/deleted
        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeleted()
        {
            var deletedTransports = await _context.Transportations
                .Where(t => t.IsDeleted)
                .Select(t => new DeletedTransportationDto
                {
                    ActiveVehicle = t.ActiveVehicle,
                    LoadId = t.CargoID,
                    VehicleId = t.VehicleId,

                    IsDeleted = t.IsDeleted,
                    DeletedAt = t.DeletedAt
                })
                .ToListAsync();

            return Ok(deletedTransports);
        }

        // GET: api/transportations/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transportation = await _context.Transportations
                .Where(t => !t.IsDeleted && t.ActiveVehicle == id)
                .Select(t => new TransportationDto
                {
                    ActiveVehicle = t.ActiveVehicle,
                    LoadId = t.CargoID,
                    VehicleId = t.VehicleId
                })
                .FirstOrDefaultAsync();

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
                VehicleId = dto.VehicleId,
                IsDeleted = false,
                DeletedAt = null
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

            if (existing == null || existing.IsDeleted)
                return NotFound();

            existing.CargoID = dto.LoadId;
            existing.VehicleId = dto.VehicleId;

            _context.Transportations.Update(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/transportations/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transportation = await _context.Transportations.FindAsync(id);

            if (transportation == null || transportation.IsDeleted)
                return NotFound();

            transportation.IsDeleted = true;
            transportation.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/transportations/restore/{id}
        [HttpPost("restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            var transportation = await _context.Transportations.FindAsync(id);

            if (transportation == null)
                return NotFound();

            if (!transportation.IsDeleted)
                return BadRequest("Перевозка не удалена");

            transportation.IsDeleted = false;
            transportation.DeletedAt = null;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}