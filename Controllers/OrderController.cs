using Microsoft.AspNetCore.Mvc;
using CCAPI.Models;
using Microsoft.EntityFrameworkCore;
using CCAPI.DTO;

namespace CCAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _context.Order.ToListAsync();
            return Ok(orders);
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        // POST: api/orders
        
        [HttpPost]
        public async Task<IActionResult> Create(OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = new Orders
            {
                IDClient = orderDto.IDClient,
                Status = orderDto.Status,
                Price = orderDto.Price,
                Date = orderDto.Date
            };

            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = order.ID }, order);
        }
       
        // PUT: api/orders/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Orders order)
        {
            if (id != order.ID)
                return BadRequest();

            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
                return NotFound();

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}