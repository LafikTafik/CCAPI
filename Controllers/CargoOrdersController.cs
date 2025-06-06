using Microsoft.AspNetCore.Mvc;
using CCAPI.Models;
using CCAPI.DTO.defaultt;
using Microsoft.EntityFrameworkCore;

namespace CCAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargoOrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CargoOrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/cargoorders
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var links = await _context.CargoOrders
                .Select(co => new CargoOrdersDto
                {
                    CargoID = co.CargoID,
                    OrderID = co.OrderID
                })
                .ToListAsync();

            return Ok(links);
        }

        // GET /api/cargoorders/5/10
        [HttpGet("{cargoId}/{orderId}")]
        public async Task<IActionResult> GetByCompositeKey(int cargoId, int orderId)
        {
            var link = await _context.CargoOrders
                .Where(co => co.CargoID == cargoId && co.OrderID == orderId)
                .FirstOrDefaultAsync();

            if (link == null) return NotFound();

            var dto = new CargoOrdersDto
            {
                CargoID = link.CargoID,
                OrderID = link.OrderID
            };

            return Ok(dto);
        }

        // POST /api/cargoorders
        [HttpPost]
        public async Task<IActionResult> Create(CargoOrdersDto dto)
        {
            var link = new CargoOrders
            {
                CargoID = dto.CargoID,
                OrderID = dto.OrderID
            };

            _context.CargoOrders.Add(link);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByCompositeKey), new { cargoId = dto.CargoID, orderId = dto.OrderID }, dto);
        }

        // DELETE /api/cargoorders/5/10
        [HttpDelete("{cargoId}/{orderId}")]
        public async Task<IActionResult> Delete(int cargoId, int orderId)
        {
            var link = await _context.CargoOrders
                .Where(co => co.CargoID == cargoId && co.OrderID == orderId)
                .FirstOrDefaultAsync();

            if (link == null) return NotFound();

            _context.CargoOrders.Remove(link);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}