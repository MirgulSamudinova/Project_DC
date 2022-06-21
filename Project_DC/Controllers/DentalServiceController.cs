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
    public class DentalServiceController : Controller
    {
        private readonly DBContext _context;

        public DentalServiceController(DBContext context)
        {
            _context = context;
        }

        // GET: DentalService
        public async Task<IActionResult> Index()
        {
            var DBContext = _context.DentalServices.Include(d => d._DentalServiceGroup);
            return View(await DBContext.ToListAsync());
        }

        // GET: DentalService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DentalServices == null)
            {
                return NotFound();
            }

            var dentalService = await _context.DentalServices
                .Include(d => d._DentalServiceGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dentalService == null)
            {
                return NotFound();
            }

            return View(dentalService);
        }

        // GET: DentalService/Create
        public IActionResult Create()
        {
            ViewData["DentalServiceGroupId"] = new SelectList(_context.DentalServiceGroups, "Id", "DentalServiceGroupName");
            ViewData["ToothStateId"] = new SelectList(_context.ToothStates, "Id", "ToothStateName");
            return View();
        }

        // POST: DentalService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NameOfService,DentalServiceGroupId,IsToothService,ToothStateId,Price")] DentalService dentalService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dentalService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DentalServiceGroupId"] = new SelectList(_context.DentalServiceGroups, "Id", "DentalServiceGroupName", dentalService.DentalServiceGroupId);
            ViewData["ToothStateId"] = new SelectList(_context.ToothStates, "Id", "ToothStateName", dentalService.ToothStateId);
            return View(dentalService);
        }

        // GET: DentalService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DentalServices == null)
            {
                return NotFound();
            }

            var dentalService = await _context.DentalServices.FindAsync(id);
            if (dentalService == null)
            {
                return NotFound();
            }
            ViewData["DentalServiceGroupId"] = new SelectList(_context.DentalServiceGroups, "Id", "DentalServiceGroupName", dentalService.DentalServiceGroupId);
            ViewData["ToothStateId"] = new SelectList(_context.ToothStates, "Id", "ToothStateName", dentalService.ToothStateId);
            return View(dentalService);
        }

        // POST: DentalService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameOfService,DentalServiceGroupId,IsToothService,ToothStateId,Price")] DentalService dentalService)
        {
            if (id != dentalService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(dentalService.ToothStateId == 0)
                    {
                        dentalService.ToothStateId = null;
                    }
                    _context.Update(dentalService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DentalServiceExists(dentalService.Id))
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
            ViewData["DentalServiceGroupId"] = new SelectList(_context.DentalServiceGroups, "Id", "DentalServiceGroupName", dentalService.DentalServiceGroupId);
            ViewData["ToothStateId"] = new SelectList(_context.ToothStates, "Id", "ToothStateName", dentalService.ToothStateId);

            return View(dentalService);
        }

        // GET: DentalService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DentalServices == null)
            {
                return NotFound();
            }

            var dentalService = await _context.DentalServices
                .Include(d => d._DentalServiceGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dentalService == null)
            {
                return NotFound();
            }

            return View(dentalService);
        }

        // POST: DentalService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DentalServices == null)
            {
                return Problem("Entity set 'DBContext.DentalServices'  is null.");
            }
            var dentalService = await _context.DentalServices.FindAsync(id);
            if (dentalService != null)
            {
                _context.DentalServices.Remove(dentalService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DentalServiceExists(int id)
        {
          return (_context.DentalServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
