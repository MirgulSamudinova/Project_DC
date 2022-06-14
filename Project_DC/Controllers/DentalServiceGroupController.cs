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
    public class DentalServiceGroupController : Controller
    {
        private readonly DBContext _context;

        public DentalServiceGroupController(DBContext context)
        {
            _context = context;
        }

        // GET: DentalServiceGroup
        public async Task<IActionResult> Index()
        {
              return _context.DentalServiceGroups != null ? 
                          View(await _context.DentalServiceGroups.ToListAsync()) :
                          Problem("Entity set 'DBContext.DentalServiceGroup'  is null.");
        }

        // GET: DentalServiceGroup/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DentalServiceGroups == null)
            {
                return NotFound();
            }

            var dentalServiceGroup = await _context.DentalServiceGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dentalServiceGroup == null)
            {
                return NotFound();
            }

            return View(dentalServiceGroup);
        }

        // GET: DentalServiceGroup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DentalServiceGroup/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DentalServiceGroupName")] DentalServiceGroup dentalServiceGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dentalServiceGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dentalServiceGroup);
        }

        // GET: DentalServiceGroup/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DentalServiceGroups == null)
            {
                return NotFound();
            }

            var dentalServiceGroup = await _context.DentalServiceGroups.FindAsync(id);
            if (dentalServiceGroup == null)
            {
                return NotFound();
            }
            return View(dentalServiceGroup);
        }

        // POST: DentalServiceGroup/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DentalServiceGroupName")] DentalServiceGroup dentalServiceGroup)
        {
            if (id != dentalServiceGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dentalServiceGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DentalServiceGroupExists(dentalServiceGroup.Id))
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
            return View(dentalServiceGroup);
        }

        // GET: DentalServiceGroup/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DentalServiceGroups == null)
            {
                return NotFound();
            }

            var dentalServiceGroup = await _context.DentalServiceGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dentalServiceGroup == null)
            {
                return NotFound();
            }

            return View(dentalServiceGroup);
        }

        // POST: DentalServiceGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DentalServiceGroups == null)
            {
                return Problem("Entity set 'DBContext.DentalServiceGroup'  is null.");
            }
            var dentalServiceGroup = await _context.DentalServiceGroups.FindAsync(id);
            if (dentalServiceGroup != null)
            {
                _context.DentalServiceGroups.Remove(dentalServiceGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DentalServiceGroupExists(int id)
        {
          return (_context.DentalServiceGroups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
