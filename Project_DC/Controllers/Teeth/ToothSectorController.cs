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
    public class ToothSectorController : Controller
    {
        private readonly DBContext _context;

        public ToothSectorController(DBContext context)
        {
            _context = context;
        }

        // GET: ToothSectors
        public async Task<IActionResult> Index()
        {
              return _context.ToothSectors != null ? 
                          View(await _context.ToothSectors.ToListAsync()) :
                          Problem("Entity set 'DBContext.ToothSector'  is null.");
        }

        // GET: ToothSectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToothSectors == null)
            {
                return NotFound();
            }

            var toothSector = await _context.ToothSectors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toothSector == null)
            {
                return NotFound();
            }

            return View(toothSector);
        }

        // GET: ToothSectors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToothSectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NumberOfSector")] ToothSector toothSector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toothSector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toothSector);
        }

        // GET: ToothSectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ToothSectors == null)
            {
                return NotFound();
            }

            var toothSector = await _context.ToothSectors.FindAsync(id);
            if (toothSector == null)
            {
                return NotFound();
            }
            return View(toothSector);
        }

        // POST: ToothSectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NumberOfSector")] ToothSector toothSector)
        {
            if (id != toothSector.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toothSector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToothSectorExists(toothSector.Id))
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
            return View(toothSector);
        }

        // GET: ToothSectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ToothSectors == null)
            {
                return NotFound();
            }

            var toothSector = await _context.ToothSectors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toothSector == null)
            {
                return NotFound();
            }

            return View(toothSector);
        }

        // POST: ToothSectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ToothSectors == null)
            {
                return Problem("Entity set 'DBContext.ToothSector'  is null.");
            }
            var toothSector = await _context.ToothSectors.FindAsync(id);
            if (toothSector != null)
            {
                _context.ToothSectors.Remove(toothSector);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToothSectorExists(int id)
        {
          return (_context.ToothSectors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
