using Microsoft.AspNetCore.Mvc;
using CCAPI.Models;
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
            var cargos = await _context.Cargo.ToListAsync();
            return Ok(cargos);
        }

        // GET: api/cargos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cargo = await _context.Cargo.FindAsync(id);

            if (cargo == null)
                return NotFound();

            return Ok(cargo);
        }

        public class CreateCargoDto
        {
            public required string Weight { get; set; }
            public required string Dimensions { get; set; }
            public string? Descriptions { get; set; }

            public int? OrderID { get; set; }
        }



        // POST: api/cargos
        [HttpPost]
        public async Task<IActionResult> Create(CreateCargoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cargo = new Cargos
            {
                Weight = dto.Weight,
                Dimensions = dto.Dimensions,
                Descriptions = dto.Descriptions,
                OrderId = dto.OrderID
            };

            _context.Cargo.Add(cargo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = cargo.ID }, cargo);
        }

        // PUT: api/cargos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Cargos cargo)
        {
            if (id != cargo.ID)
                return BadRequest();

            _context.Entry(cargo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/cargos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cargo = await _context.Cargo.FindAsync(id);

            if (cargo == null)
                return NotFound();

            _context.Cargo.Remove(cargo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}