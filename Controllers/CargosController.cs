using Microsoft.AspNetCore.Mvc;
using CCAPI.Models;
using CCAPI.DTO.defaultt;
using CCAPI.DTO.deleted;
using Microsoft.EntityFrameworkCore;

namespace CCAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CargosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/cargos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cargos = await _context.Cargo
                .Where(c => !c.IsDeleted)
                .Select(c => new CargoDto
                {
                    ID = c.ID,
                    OrderID = c.OrderId,
                    Weight = c.Weight,
                    Dimensions = c.Dimensions,
                    Descriptions = c.Descriptions
                })
                .ToListAsync();

            return Ok(cargos);
        }

        // GET: api/cargos/deleted
        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeleted()
        {
            var deletedCargos = await _context.Cargo
                .Where(c => c.IsDeleted)
                .Select(c => new DeletedCargoDto
                {
                    ID = c.ID,
                    OrderID = c.OrderId,
                    Weight = c.Weight,
                    Dimensions = c.Dimensions,
                    Descriptions = c.Descriptions,
                    IsDeleted = c.IsDeleted,
                    DeletedAt = c.DeletedAt
                })
                .ToListAsync();

            return Ok(deletedCargos);
        }

        // GET: api/cargos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cargo = await _context.Cargo
                .Where(c => !c.IsDeleted && c.ID == id)
                .Select(c => new CargoDto
                {
                    ID = c.ID,
                    OrderID = c.OrderId,
                    Weight = c.Weight,
                    Dimensions = c.Dimensions,
                    Descriptions = c.Descriptions
                })
                .FirstOrDefaultAsync();

            if (cargo == null)
                return NotFound();

            return Ok(cargo);
        }

        // POST: api/cargos
        [HttpPost]
        public async Task<IActionResult> Create(CargoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cargo = new Cargos
            {
                OrderId = dto.OrderID,
                Weight = dto.Weight,
                Dimensions = dto.Dimensions,
                Descriptions = dto.Descriptions,
                IsDeleted = false,
                DeletedAt = null
            };

            _context.Cargo.Add(cargo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = cargo.ID }, cargo);
        }

        // PUT: api/cargos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CargoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _context.Cargo.FindAsync(id);

            if (existing == null || existing.IsDeleted)
                return NotFound();
            existing.OrderId = dto.OrderID;
            existing.Weight = dto.Weight;
            existing.Dimensions = dto.Dimensions;
            existing.Descriptions = dto.Descriptions;

            _context.Cargo.Update(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/cargos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cargo = await _context.Cargo.FindAsync(id);

            if (cargo == null || cargo.IsDeleted)
                return NotFound();

            cargo.IsDeleted = true;
            cargo.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/cargos/restore/{id}
        [HttpPost("restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            var cargo = await _context.Cargo.FindAsync(id);

            if (cargo == null)
                return NotFound();

            if (!cargo.IsDeleted)
                return BadRequest("Груз не удален");

            cargo.IsDeleted = false;
            cargo.DeletedAt = null;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}