using Microsoft.AspNetCore.Mvc;
using CCAPI.Models;
using CCAPI.DTO.defaultt;
using CCAPI.DTO.deleted ;
using Microsoft.EntityFrameworkCore;

namespace CCAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/clients
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _context.Clients
                .Where(c => !c.IsDeleted)
                .Select(c => new ClientDto
                {
                    ID = c.ID,
                    Name = c.Name,
                    Surname = c.Surname,
                    Phone = c.Phone,
                    Email = c.Email,
                    Address = c.Address,
                })
                .ToListAsync();

            return Ok(clients);
        }

        // GET: api/clients/deleted
        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeleted()
        {
            var deletedClients = await _context.Clients
                .Where(c => c.IsDeleted)
                .Select(c => new DeletedClientDto
                {
                    ID = c.ID,
                    Name = c.Name,
                    Surname = c.Surname,
                    Phone = c.Phone,
                    Email = c.Email,
                    Address = c.Address,
                    IsDeleted = c.IsDeleted,
                    DeletedAt = c.DeletedAt
                })
                .ToListAsync();

            return Ok(deletedClients);
        }

        // GET: api/clients/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _context.Clients
                .Where(c => !c.IsDeleted && c.ID == id)
                .Select(c => new ClientDto
                {
                    ID = c.ID,
                    Name = c.Name,
                    Surname = c.Surname,
                    Phone = c.Phone,
                    Email = c.Email,
                    Address = c.Address,

                })
                .FirstOrDefaultAsync();

            if (client == null)
                return NotFound();

            return Ok(client);
        }

        // PUT: api/clients/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ClientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingClient = await _context.Clients.FindAsync(id);

            if (existingClient == null)
                return NotFound();

            existingClient.Name = dto.Name;
            existingClient.Surname = dto.Surname;
            existingClient.Phone = dto.Phone;
            existingClient.Email = dto.Email;
            existingClient.Address = dto.Address;

            _context.Clients.Update(existingClient);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/clients/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null || client.IsDeleted)
                return NotFound();

            client.IsDeleted = true;
            client.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/clients
        [HttpPost]
        public async Task<IActionResult> Create(ClientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var client = new Client
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = client.ID }, client);
        }

        // POST: api/clients/restore/{id}
        [HttpPost("restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
                return NotFound();

            if (!client.IsDeleted)
                return BadRequest("Клиент не удален");

            client.IsDeleted = false;
            client.DeletedAt = null;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}