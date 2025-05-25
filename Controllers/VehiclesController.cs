using Microsoft.AspNetCore.Mvc;
using CCAPI.Models;
using CCAPI.DTO.defaultt;
using CCAPI.DTO.deleted;
using Microsoft.EntityFrameworkCore;

namespace CCAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VehiclesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/vehicles
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _context.Vehicles
                .Where(v => !v.IsDeleted)
                .Select(v => new VehicleDto
                {
                    ID = v.ID,
                    Type = v.Type,
                    Capacity = v.Capacity,
                    VehicleNum = v.VehicleNum,
                    DriverId = v.DriverId
                })
                .ToListAsync();

            return Ok(vehicles);
        }

        // GET: api/vehicles/deleted
        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeleted()
        {
            var deletedVehicles = await _context.Vehicles
                .Where(v => v.IsDeleted)
                .Select(v => new DeletedVehicleDto
                {
                    ID = v.ID,
                    Type = v.Type,
                    Capacity = v.Capacity,
                    VehicleNum = v.VehicleNum,
                    DriverId = v.DriverId,

                    IsDeleted = v.IsDeleted,
                    DeletedAt = v.DeletedAt
                })
                .ToListAsync();

            return Ok(deletedVehicles);
        }

        // GET: api/vehicles/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vehicle = await _context.Vehicles
                .Where(v => !v.IsDeleted && v.ID == id)
                .Select(v => new VehicleDto
                {
                    ID = v.ID,
                    Type = v.Type,
                    Capacity = v.Capacity,
                    VehicleNum = v.VehicleNum,
                    DriverId = v.DriverId
                })
                .FirstOrDefaultAsync();

            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }

        // POST: api/vehicles
        [HttpPost]
        public async Task<IActionResult> Create(VehicleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = new Vehicle
            {
                Type = dto.Type,
                Capacity = dto.Capacity,
                VehicleNum = dto.VehicleNum,
                DriverId = dto.DriverId,
                IsDeleted = false,
                DeletedAt = null
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = vehicle.ID }, vehicle);
        }

        // PUT: api/vehicles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, VehicleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _context.Vehicles.FindAsync(id);

            if (existing == null || existing.IsDeleted)
                return NotFound();

            existing.Type = dto.Type;
            existing.Capacity = dto.Capacity;
            existing.VehicleNum = dto.VehicleNum;
            existing.DriverId = dto.DriverId;

            _context.Vehicles.Update(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/vehicles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null || vehicle.IsDeleted)
                return NotFound();

            vehicle.IsDeleted = true;
            vehicle.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/vehicles/restore/{id}
        [HttpPost("restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
                return NotFound();

            if (!vehicle.IsDeleted)
                return BadRequest("Транспорт не удален");

            vehicle.IsDeleted = false;
            vehicle.DeletedAt = null;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}