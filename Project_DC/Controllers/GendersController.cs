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
    public class GendersController : Controller
    {
        private readonly DBContext _context;

        public GendersController(DBContext context)
        {
            _context = context;
        }

        // GET: Genders
        public async Task<IActionResult> Index()
        {
              return _context.Genders != null ? 
                          View(await _context.Genders.ToListAsync()) :
                          Problem("Entity set 'DBContext.Genders'  is null.");
        }

        // GET: Genders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Genders == null)
            {
                return NotFound();
            }

            var genders = await _context.Genders
                .FirstOrDefaultAsync(m => m.id_gender == id);
            if (genders == null)
            {
                return NotFound();
            }

            return View(genders);
        }

        // GET: Genders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_gender,gender")] Genders genders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genders);
        }

        // GET: Genders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Genders == null)
            {
                return NotFound();
            }

            var genders = await _context.Genders.FindAsync(id);
            if (genders == null)
            {
                return NotFound();
            }
            return View(genders);
        }

        // POST: Genders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_gender,gender")] Genders genders)
        {
            if (id != genders.id_gender)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GendersExists(genders.id_gender))
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
            return View(genders);
        }

        // GET: Genders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Genders == null)
            {
                return NotFound();
            }

            var genders = await _context.Genders
                .FirstOrDefaultAsync(m => m.id_gender == id);
            if (genders == null)
            {
                return NotFound();
            }

            return View(genders);
        }

        // POST: Genders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Genders == null)
            {
                return Problem("Entity set 'DBContext.Genders'  is null.");
            }
            var genders = await _context.Genders.FindAsync(id);
            if (genders != null)
            {
                _context.Genders.Remove(genders);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GendersExists(int id)
        {
          return (_context.Genders?.Any(e => e.id_gender == id)).GetValueOrDefault();
        }
    }
}
