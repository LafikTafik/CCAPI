using Microsoft.AspNetCore.Mvc;
using CCAPI.Models;
using CCAPI.DTO;
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
            var vehicles = await _context.Vehicles.ToListAsync();
            return Ok(vehicles);
        }

        // GET: api/vehicles/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

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
                DriverId = dto.DriverId
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

            var existingVehicle = await _context.Vehicles.FindAsync(id);

            if (existingVehicle == null)
                return NotFound();

            existingVehicle.Type = dto.Type;
            existingVehicle.Capacity = dto.Capacity;
            existingVehicle.VehicleNum = dto.VehicleNum;
            existingVehicle.DriverId = dto.DriverId;

            _context.Vehicles.Update(existingVehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/vehicles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
                return NotFound();

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}