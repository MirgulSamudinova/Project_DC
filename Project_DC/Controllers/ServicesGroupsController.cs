using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Project_DC.Models
{
    public class ServicesGroupsController : Controller
    {
        private readonly DBContext _context;

        public ServicesGroupsController(DBContext context)
        {
            _context = context;
        }

        // GET: ServicesGroups
        public async Task<IActionResult> Index()
        {
              return _context.ServicesGroup != null ? 
                          View(await _context.ServicesGroup.ToListAsync()) :
                          Problem("Entity set 'DBContext.ServicesGroup'  is null.");
        }

        // GET: ServicesGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ServicesGroup == null)
            {
                return NotFound();
            }

            var servicesGroup = await _context.ServicesGroup
                .FirstOrDefaultAsync(m => m.IdGroup == id);
            if (servicesGroup == null)
            {
                return NotFound();
            }

            return View(servicesGroup);
        }

        // GET: ServicesGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServicesGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGroup,GroupName")] ServicesGroup servicesGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servicesGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servicesGroup);
        }

        // GET: ServicesGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ServicesGroup == null)
            {
                return NotFound();
            }

            var servicesGroup = await _context.ServicesGroup.FindAsync(id);
            if (servicesGroup == null)
            {
                return NotFound();
            }
            return View(servicesGroup);
        }

        // POST: ServicesGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGroup,GroupName")] ServicesGroup servicesGroup)
        {
            if (id != servicesGroup.IdGroup)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicesGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicesGroupExists(servicesGroup.IdGroup))
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
            return View(servicesGroup);
        }

        // GET: ServicesGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ServicesGroup == null)
            {
                return NotFound();
            }

            var servicesGroup = await _context.ServicesGroup
                .FirstOrDefaultAsync(m => m.IdGroup == id);
            if (servicesGroup == null)
            {
                return NotFound();
            }

            return View(servicesGroup);
        }

        // POST: ServicesGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ServicesGroup == null)
            {
                return Problem("Entity set 'DBContext.ServicesGroup'  is null.");
            }
            var servicesGroup = await _context.ServicesGroup.FindAsync(id);
            if (servicesGroup != null)
            {
                _context.ServicesGroup.Remove(servicesGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicesGroupExists(int id)
        {
          return (_context.ServicesGroup?.Any(e => e.IdGroup == id)).GetValueOrDefault();
        }
    }
}
