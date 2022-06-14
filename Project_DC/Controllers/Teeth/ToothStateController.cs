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
    public class ToothStateController : Controller
    {
        private readonly DBContext _context;

        public ToothStateController(DBContext context)
        {
            _context = context;
        }

        // GET: ToothState
        public async Task<IActionResult> Index()
        {
              return _context.ToothStates != null ? 
                          View(await _context.ToothStates.ToListAsync()) :
                          Problem("Entity set 'DBContext.ToothStates'  is null.");
        }

        // GET: ToothState/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToothStates == null)
            {
                return NotFound();
            }

            var toothState = await _context.ToothStates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toothState == null)
            {
                return NotFound();
            }

            return View(toothState);
        }

        // GET: ToothState/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToothState/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ToothStateName,ToothStateColor")] ToothState toothState)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toothState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toothState);
        }

        // GET: ToothState/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ToothStates == null)
            {
                return NotFound();
            }

            var toothState = await _context.ToothStates.FindAsync(id);
            if (toothState == null)
            {
                return NotFound();
            }
            return View(toothState);
        }

        // POST: ToothState/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ToothStateName,ToothStateColor")] ToothState toothState)
        {
            if (id != toothState.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toothState);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToothStateExists(toothState.Id))
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
            return View(toothState);
        }

        // GET: ToothState/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ToothStates == null)
            {
                return NotFound();
            }

            var toothState = await _context.ToothStates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toothState == null)
            {
                return NotFound();
            }

            return View(toothState);
        }

        // POST: ToothState/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ToothStates == null)
            {
                return Problem("Entity set 'DBContext.ToothStates'  is null.");
            }
            var toothState = await _context.ToothStates.FindAsync(id);
            if (toothState != null)
            {
                _context.ToothStates.Remove(toothState);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToothStateExists(int id)
        {
          return (_context.ToothStates?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
