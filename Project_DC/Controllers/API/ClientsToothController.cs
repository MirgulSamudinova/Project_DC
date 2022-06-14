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
    public class ClientsToothController : ControllerBase
    {
        private readonly DBContext _context;

        public ClientsToothController(DBContext context)
        {
            _context = context;
        }

        // GET: api/ClientsTooth
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientsTooth>>> GetClientsTeeth()
        {
          if (_context.ClientsTeeth == null)
          {
              return NotFound();
          }
            return await _context.ClientsTeeth.ToListAsync();
        }

        // GET: api/ClientsTooth/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ClientsTooth>>> GetClientsTeeth(int id)
        {
          if (_context.ClientsTeeth == null)
          {
              return NotFound();
          }
            var clientsTooth = _context.ClientsTeeth
                .Include(x=>x._ToothState)
                .Include(x=>x._Client)
                .Include(x=>x._Tooth).ThenInclude(x=>x._ToothSector)
                .Where(x => x._Client != null && x.ClientId == id).ToList();

            if (clientsTooth == null)
            {
                return NotFound();
            }

            return clientsTooth;
        }

        // GET: api/ClientsTooth/5/1
        [HttpGet("{id}/{toothId}")]
        public async Task<ActionResult<ClientsTooth>> GetClientsTooth(int id, string toothId)
        {
            if (_context.ClientsTeeth == null)
            {
                return NotFound();
            }
            var clientsTeeth = _context.ClientsTeeth
                .Include(x => x._ToothState)
                .Include(x => x._Client)
                .Include(x => x._Tooth).ThenInclude(x => x._ToothSector)
                .Where(x => x._Client != null && x.ClientId == id ).ToList();

            var clientsTooth = clientsTeeth == null ? null : clientsTeeth.Find(x => x._Tooth != null && x._Tooth.ToothId == toothId);

            if (clientsTeeth == null || clientsTooth == null)
            {
                ClientsTooth clientsToothNew = new ClientsTooth();
                int curNo = Int32.Parse(toothId.Substring(1, 1));
                int sector = Int32.Parse(toothId.Substring(0, 1));
                clientsToothNew.ClientId = id;
                var toothList = _context.Teeth
                    .Include(x=>x._ToothSector)
                    .Where(x => x._ToothSector.NumberOfSector == sector && x.CurrentNumber == curNo).ToList();

                Tooth tooth = toothList == null || toothList.Count() == 0 ? null : toothList[0];

                if (tooth == null)
                {
                    return NotFound();
                }

                clientsToothNew.ToothId = tooth.Id;
                clientsToothNew._Tooth = tooth;
                clientsToothNew.ToothStateId = 1;

                if (_context.ClientsTeeth == null)
                {
                    return Problem("Entity set 'DBContext.ClientsTeeth'  is null.");
                }
                _context.ClientsTeeth.Add(clientsToothNew);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetClientsTooth", new { id = clientsToothNew.ClientId, toothId = clientsToothNew._Tooth.ToothId }, clientsToothNew);

            }

            return clientsTooth;
            
        }

        // PUT: api/ClientsTooth/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientsTooth(int id, ClientsTooth clientsTooth)
        {
            if (id != clientsTooth.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientsTooth).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientsToothExists(id))
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

        // POST: api/ClientsTooth
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientsTooth>> PostClientsTooth(ClientsTooth clientsTooth)
        {
          if (_context.ClientsTeeth == null)
          {
              return Problem("Entity set 'DBContext.ClientsTeeth'  is null.");
          }
            _context.ClientsTeeth.Add(clientsTooth);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientsTooth", new { id = clientsTooth.Id }, clientsTooth);
        }

        private bool ClientsToothExists(int id)
        {
            return (_context.ClientsTeeth?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
