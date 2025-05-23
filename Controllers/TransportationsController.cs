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
            var transportations = await _context.Shipping.ToListAsync();
            return Ok(transportations);
        }

        // GET: api/transportations/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transportation = await _context.Shipping.FindAsync(id);

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

            // Загружаем груз и транспорт из БД по ID
            var cargo = await _context.Cargo.FindAsync(dto.LoadId);
            var vehicle = await _context.Vehicles.FindAsync(dto.VehicleId);

            if (cargo == null || vehicle == null)
                return BadRequest("Неверный LoadId или VehicleId");

            var transportation = new Transportation
            {
                CargoID = dto.LoadId,
                Load = cargo,
                VehicleId = dto.VehicleId,
                Vehicle = vehicle
            };

            _context.Shipping.Add(transportation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = transportation.CargoID }, transportation);
        }

        // PUT: api/transportations/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TransportationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _context.Shipping.FindAsync(id);

            if (existing == null)
                return NotFound();

            var cargo = await _context.Cargo.FindAsync(dto.LoadId);
            var vehicle = await _context.Vehicles.FindAsync(dto.VehicleId);

            if (cargo == null || vehicle == null)
                return BadRequest("Неверный LoadId или VehicleId");

            existing.CargoID = dto.LoadId;
            existing.Load = cargo;
            existing.VehicleId = dto.VehicleId;
            existing.Vehicle = vehicle;

            _context.Shipping.Update(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/transportations/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transportation = await _context.Shipping.FindAsync(id);

            if (transportation == null)
                return NotFound();

            _context.Shipping.Remove(transportation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}