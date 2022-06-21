using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_DC.Models;

namespace Project_DC.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralServiceController : ControllerBase
    {
        private readonly DBContext _context;

        public GeneralServiceController(DBContext context)
        {
            _context = context;
        }

        // GET: api/GeneralServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneralService>>> GetGeneralService()
        {
          if (_context.GeneralService == null)
          {
              return NotFound();
          }
            return await _context.GeneralService.ToListAsync();
        }

        // GET: api/GeneralServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralService>> GetGeneralService(int id)
        {
          if (_context.GeneralService == null)
          {
              return NotFound();
          }
            var generalService = await _context.GeneralService.FindAsync(id);

            if (generalService == null)
            {
                return NotFound();
            }

            return generalService;
        }

        // PUT: api/GeneralServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeneralService(int id, GeneralService generalService)
        {
            if (id != generalService.Id)
            {
                return BadRequest();
            }

            _context.Entry(generalService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneralServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GeneralServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GeneralService>> PostGeneralService(GeneralService generalService)
        {
            if (_context.GeneralService == null)
            {
                return Problem("Entity set 'DBContext.GeneralService'  is null.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(generalService);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetGeneralService", new { id = generalService.Id }, generalService);
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                                    .SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage));
                return Problem(message);
            }
        }

                    

        // DELETE: api/GeneralServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeneralService(int id)
        {
            if (_context.GeneralService == null)
            {
                return NotFound();
            }
            var generalService = await _context.GeneralService.FindAsync(id);
            if (generalService == null)
            {
                return NotFound();
            }

            _context.GeneralService.Remove(generalService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GeneralServiceExists(int id)
        {
            return (_context.GeneralService?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
