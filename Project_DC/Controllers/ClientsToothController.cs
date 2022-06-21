using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_DC.Models;

namespace Project_DC.Controllers
{
    public class ClientsToothController : Controller
    {
        private readonly DBContext _context;

        public ClientsToothController(DBContext context)
        {
            _context = context;
        }

        // GET: ClientsTooth
        public async Task<IActionResult> Index()
        {
            var DBContext = _context.ClientsTeeth.Include(c => c._Patients).Include(c => c._Tooth).ThenInclude(c => c._ToothSector).Include(c => c._ToothState);
            return View(await DBContext.ToListAsync());
        }

        // GET: ClientsTooth/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClientsTeeth == null)
            {
                return NotFound();
            }

            var clientsTooth = await _context.ClientsTeeth
                .Include(c => c._Patients)
                .Include(c => c._Tooth)
                .Include(c => c._ToothState)
                .Include(c => c._Tooth._ToothSector)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientsTooth == null)
            {
                return NotFound();
            }

            return View(clientsTooth);
        }

        // GET: ClientsTooth/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Patients, "Id", "FIO");
            ViewData["ToothId"] = new SelectList(_context.Teeth.Include(x => x._ToothSector), "Id", "ToothDetails");
            ViewData["ToothStateId"] = new SelectList(_context.ToothStates, "Id", "ToothStateName");
            return View();
        }

        // POST: ClientsTooth/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,ToothId,ToothStateId")] ClientsTooth clientsTooth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientsTooth);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Patients, "Id", "FIO", clientsTooth.PatientsId);
            ViewData["ToothId"] = new SelectList(_context.Teeth.Include(x => x._ToothSector), "Id", "ToothDetails", clientsTooth.ToothId);
            ViewData["ToothStateId"] = new SelectList(_context.ToothStates, "Id", "ToothStateName", clientsTooth.ToothStateId);
            return View(clientsTooth);
        }

        // GET: ClientsTooth/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClientsTeeth == null)
            {
                return NotFound();
            }
            var clientsTooth = await _context.ClientsTeeth
                .Include(c => c._Patients)
                .Include(c => c._Tooth).ThenInclude(c=>c._ToothSector)
                .Include(c => c._ToothState)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientsTooth == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Patients, "Id", "FIO", clientsTooth.PatientsId);
            ViewData["ToothId"] = new SelectList(_context.Teeth.Include(x=>x._ToothSector), "Id", "ToothDetails", clientsTooth.ToothId);
            ViewData["ToothStateId"] = new SelectList(_context.ToothStates, "Id", "ToothStateName", clientsTooth.ToothStateId);
            return View(clientsTooth);
        }

        // POST: ClientsTooth/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientsId,ToothId,ToothStateId")] ClientsTooth clientsTooth)
        {
            if (id != clientsTooth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientsTooth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsToothExists(clientsTooth.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Patients, "Id", "FIO", clientsTooth.PatientsId);
            ViewData["ToothId"] = new SelectList(_context.Teeth.Include(x => x._ToothSector), "Id", "ToothId", clientsTooth.ToothId);
            ViewData["ToothStateId"] = new SelectList(_context.ToothStates, "Id", "ToothStateName", clientsTooth.ToothStateId);
            return View(clientsTooth);
        }

        // GET: ClientsTooth/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClientsTeeth == null)
            {
                return NotFound();
            }

            var clientsTooth = await _context.ClientsTeeth
                .Include(c => c._Patients)
                .Include(c => c._Tooth).ThenInclude(c => c._ToothSector)
                .Include(c => c._ToothState)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientsTooth == null)
            {
                return NotFound();
            }

            return View(clientsTooth);
        }

        // POST: ClientsTooth/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClientsTeeth == null)
            {
                return Problem("Entity set 'DBContext.ClientsTeeth'  is null.");
            }
            var clientsTooth = await _context.ClientsTeeth.FindAsync(id);
            if (clientsTooth != null)
            {
                _context.ClientsTeeth.Remove(clientsTooth);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientsToothExists(int id)
        {
          return (_context.ClientsTeeth?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
