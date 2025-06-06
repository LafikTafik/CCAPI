using Microsoft.AspNetCore.Mvc;
using CCAPI.Models;
using CCAPI.DTO.defaultt;
using Microsoft.EntityFrameworkCore;

namespace CCAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransCompController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransCompController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/transcomp
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var links = await _context.TransComp
                .Select(tc => new TransCompDto
                {
                    TransportationID = tc.TransportationID,
                    CompanyID = tc.CompanyID
                })
                .ToListAsync();

            return Ok(links);
        }

        // GET /api/transcomp/1/2
        [HttpGet("{transId}/{companyId}")]
        public async Task<IActionResult> GetByCompositeKey(int transId, int companyId)
        {
            var link = await _context.TransComp
                .Where(tc => tc.TransportationID == transId && tc.CompanyID == companyId)
                .FirstOrDefaultAsync();

            if (link == null) return NotFound();

            return Ok(new TransCompDto
            {
                TransportationID = link.TransportationID,
                CompanyID = link.CompanyID
            });
        }

        // POST /api/transcomp
        [HttpPost]
        public async Task<IActionResult> Create(TransCompDto dto)
        {
            var link = new TransComp
            {
                TransportationID = dto.TransportationID,
                CompanyID = dto.CompanyID
            };

            _context.TransComp.Add(link);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByCompositeKey), new { transId = dto.TransportationID, companyId = dto.CompanyID }, dto);
        }

        // DELETE /api/transcomp/1/2
        [HttpDelete("{transId}/{companyId}")]
        public async Task<IActionResult> Delete(int transId, int companyId)
        {
            var link = await _context.TransComp
                .Where(tc => tc.TransportationID == transId && tc.CompanyID == companyId)
                .FirstOrDefaultAsync();

            if (link == null) return NotFound();

            _context.TransComp.Remove(link);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}