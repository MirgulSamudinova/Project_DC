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
    public class ToothController : Controller
    {
        private readonly DBContext _context;

        public ToothController(DBContext context)
        {
            _context = context;
        }

        // GET: Tooth
        public async Task<IActionResult> Index()
        {
            var DBContext = _context.Teeth.Include(t => t._ToothSector).Include(t => t._ToothType);
            return View(await DBContext.ToListAsync());
        }

        // GET: Tooth/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teeth == null)
            {
                return NotFound();
            }

            var tooth = await _context.Teeth
                .Include(t => t._ToothSector)
                .Include(t => t._ToothType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tooth == null)
            {
                return NotFound();
            }

            return View(tooth);
        }


        // GET: Tooth/Create
        public IActionResult Create()
        {
            ViewData["ToothSectorId"] = new SelectList(_context.ToothSectors, "Id", "Name");
            ViewData["ToothTypeId"] = new SelectList(_context.ToothTypes, "Id", "ToothTypeName");
            return View();
        }

        // POST: Tooth/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CurrentNumber,ToothSectorId,ToothTypeId")] Tooth tooth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tooth);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ToothSectorId"] = new SelectList(_context.ToothSectors, "Id", "Name", tooth.ToothSectorId);
            ViewData["ToothTypeId"] = new SelectList(_context.ToothTypes, "Id", "ToothTypeName", tooth.ToothTypeId);
            return View(tooth);
        }

        // GET: Tooth/Copy
        public async Task<IActionResult> Copy(int? id)
        {
            if (id == null || _context.Teeth == null)
            {
                return NotFound();
            }

            var tooth = await _context.Teeth.FindAsync(id);
            Tooth newTooth = tooth.Clone() as Tooth;
            if (newTooth != null)
            {
                newTooth.Id = 0;
                ViewData["ToothSectorId"] = new SelectList(_context.ToothSectors, "Id", "Name", newTooth.ToothSectorId);
                ViewData["ToothTypeId"] = new SelectList(_context.ToothTypes, "Id", "ToothTypeName", newTooth.ToothTypeId);
                return View(newTooth);
            }
            else
            {
                return NotFound();
            }
            
        }

        // POST: Tooth/Copy
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Copy([Bind("Name,CurrentNumber,ToothSectorId,ToothTypeId")] Tooth tooth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tooth);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ToothSectorId"] = new SelectList(_context.ToothSectors, "Id", "Name", tooth.ToothSectorId);
            ViewData["ToothTypeId"] = new SelectList(_context.ToothTypes, "Id", "ToothTypeName", tooth.ToothTypeId);
            return View(tooth);
        }

        // GET: Tooth/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teeth == null)
            {
                return NotFound();
            }

            var tooth = await _context.Teeth.FindAsync(id);
            if (tooth == null)
            {
                return NotFound();
            }
            ViewData["ToothSectorId"] = new SelectList(_context.ToothSectors, "Id", "Name", tooth.ToothSectorId);
            ViewData["ToothTypeId"] = new SelectList(_context.ToothTypes, "Id", "ToothTypeName", tooth.ToothTypeId);
            return View(tooth);
        }

        // POST: Tooth/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CurrentNumber,ToothSectorId,ToothTypeId")] Tooth tooth)
        {
            if (id != tooth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tooth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToothExists(tooth.Id))
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
            ViewData["ToothSectorId"] = new SelectList(_context.ToothSectors, "Id", "Name", tooth.ToothSectorId);
            ViewData["ToothTypeId"] = new SelectList(_context.ToothTypes, "Id", "ToothTypeName", tooth.ToothTypeId);
            return View(tooth);
        }

        // GET: Tooth/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teeth == null)
            {
                return NotFound();
            }

            var tooth = await _context.Teeth
                .Include(t => t._ToothSector)
                .Include(t => t._ToothType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tooth == null)
            {
                return NotFound();
            }

            return View(tooth);
        }

        // POST: Tooth/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teeth == null)
            {
                return Problem("Entity set 'DBContext.Teeth'  is null.");
            }
            var tooth = await _context.Teeth.FindAsync(id);
            if (tooth != null)
            {
                _context.Teeth.Remove(tooth);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToothExists(int id)
        {
          return (_context.Teeth?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
