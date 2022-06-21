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
    public class ToothServiceController : ControllerBase
    {
        private readonly DBContext _context;

        public ToothServiceController(DBContext context)
        {
            _context = context;
        }

        // GET: api/ToothService
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToothService>>> GetToothServices()
        {
          if (_context.ToothServices == null)
          {
              return NotFound();
          }
            return await _context.ToothServices.ToListAsync();
        }

        // GET: api/ToothService/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToothService>> GetToothService(int id)
        {
          if (_context.ToothServices == null)
          {
              return NotFound();
          }
            var toothService = await _context.ToothServices.FindAsync(id);

            if (toothService == null)
            {
                return NotFound();
            }

            return toothService;
        }

        // PUT: api/ToothService/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToothService(int id, ToothService toothService)
        {
            if (id != toothService.Id)
            {
                return BadRequest();
            }

            _context.Entry(toothService).State = EntityState.Modified;
            if (toothService.IsToothStateChange)
            {
                var clientsTooth = await _context.ClientsTeeth.FirstAsync(id => id.Id == toothService.ClientsToothId);
                if (clientsTooth != null)
                {
                    var dentalService = await _context.DentalServices.FirstAsync(id => id.Id == toothService.DentalServiceId);
                    if (dentalService != null && dentalService.ToothStateId != null)
                    {
                        clientsTooth.ToothStateId = (int)dentalService.ToothStateId;
                        _context.Entry(clientsTooth).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }

                }
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToothServiceExists(id))
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

        // POST: api/ToothService
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToothService>> PostToothService(ToothService toothService)
        {
          if (_context.ToothServices == null)
          {
              return Problem("Entity set 'DBContext.ToothServices'  is null.");
          }
            _context.ToothServices.Add(toothService);
            if (toothService.IsToothStateChange)
            {
                var clientsTooth = await _context.ClientsTeeth.FirstAsync(id => id.Id == toothService.ClientsToothId);
                if (clientsTooth != null)
                {
                    var dentalService = await _context.DentalServices.FirstAsync(id => id.Id == toothService.DentalServiceId);
                    if (dentalService != null && dentalService.ToothStateId != null)
                    {
                        clientsTooth.ToothStateId = (int)dentalService.ToothStateId;
                        _context.Update(clientsTooth);
                        await _context.SaveChangesAsync();
                    }

                }
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToothService", new { id = toothService.Id }, toothService);
        }

        // DELETE: api/ToothService/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToothService(int id)
        {
            if (_context.ToothServices == null)
            {
                return NotFound();
            }
            var toothService = await _context.ToothServices.FindAsync(id);
            if (toothService == null)
            {
                return NotFound();
            }

            _context.ToothServices.Remove(toothService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToothServiceExists(int id)
        {
            return (_context.ToothServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
