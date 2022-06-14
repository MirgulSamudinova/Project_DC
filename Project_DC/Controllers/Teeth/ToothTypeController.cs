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
    public class ToothTypeController : Controller
    {
        private readonly DBContext _context;

        public ToothTypeController(DBContext context)
        {
            _context = context;
        }

        // GET: ToothType
        public async Task<IActionResult> Index()
        {
              return _context.ToothTypes != null ? 
                          View(await _context.ToothTypes.ToListAsync()) :
                          Problem("Entity set 'DBContext.ToothTypes'  is null.");
        }

        // GET: ToothType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToothTypes == null)
            {
                return NotFound();
            }

            var toothType = await _context.ToothTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toothType == null)
            {
                return NotFound();
            }

            return View(toothType);
        }

        // GET: ToothType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToothType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ToothTypeName")] ToothType toothType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toothType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toothType);
        }

        // GET: ToothType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ToothTypes == null)
            {
                return NotFound();
            }

            var toothType = await _context.ToothTypes.FindAsync(id);
            if (toothType == null)
            {
                return NotFound();
            }
            return View(toothType);
        }

        // POST: ToothType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ToothTypeName")] ToothType toothType)
        {
            if (id != toothType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toothType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToothTypeExists(toothType.Id))
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
            return View(toothType);
        }

        // GET: ToothType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ToothTypes == null)
            {
                return NotFound();
            }

            var toothType = await _context.ToothTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toothType == null)
            {
                return NotFound();
            }

            return View(toothType);
        }

        // POST: ToothType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ToothTypes == null)
            {
                return Problem("Entity set 'DBContext.ToothTypes'  is null.");
            }
            var toothType = await _context.ToothTypes.FindAsync(id);
            if (toothType != null)
            {
                _context.ToothTypes.Remove(toothType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToothTypeExists(int id)
        {
          return (_context.ToothTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
