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
    public class GeneralServiceController : Controller
    {
        private readonly DBContext _context;

        public GeneralServiceController(DBContext context)
        {
            _context = context;
        }

        // GET: GeneralService
        public async Task<IActionResult> Index()
        {
            var DBContext = _context.GeneralService.Include(g => g._DentalService);
            return View(await DBContext.ToListAsync());
        }

        // GET: GeneralService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GeneralService == null)
            {
                return NotFound();
            }

            var generalService = await _context.GeneralService
                .Include(g => g._DentalService)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (generalService == null)
            {
                return NotFound();
            }

            return View(generalService);
        }

        // GET: GeneralService/Create
        public IActionResult Create()
        {
            ViewData["DentalServiceId"] = new SelectList(GetDentalServices(), "Id", "NameOfService");
            return View();
        }

        // POST: GeneralService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DentalServiceId,Comment,Price")] GeneralService generalService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(generalService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DentalServiceId"] = new SelectList(_context.DentalServices, "Id", "NameOfService", generalService.DentalServiceId);
            return View(generalService);
        }

        public List<DentalService> GetDentalServices()
        {
            var dentalServices = (from service in _context.DentalServices where service.IsToothService == false
                                  select service).ToList();
            return dentalServices;
        }

        // GET: GeneralService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GeneralService == null)
            {
                return NotFound();
            }

            var generalService = await _context.GeneralService.FindAsync(id);
            if (generalService == null)
            {
                return NotFound();
            }
            

            ViewData["DentalServiceId"] = new SelectList(GetDentalServices(), "Id", "NameOfService", generalService.DentalServiceId);
            return View(generalService);
        }

        // POST: GeneralService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DentalServiceId,Comment,Price")] GeneralService generalService)
        {
            if (id != generalService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(generalService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneralServiceExists(generalService.Id))
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
            ViewData["DentalServiceId"] = new SelectList(GetDentalServices(), "Id", "NameOfService", generalService.DentalServiceId);
            return View(generalService);
        }

        // GET: GeneralService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GeneralService == null)
            {
                return NotFound();
            }

            var generalService = await _context.GeneralService
                .Include(g => g._DentalService)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (generalService == null)
            {
                return NotFound();
            }

            return View(generalService);
        }

        // POST: GeneralService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GeneralService == null)
            {
                return Problem("Entity set 'DBContext.GeneralService'  is null.");
            }
            var generalService = await _context.GeneralService.FindAsync(id);
            if (generalService != null)
            {
                _context.GeneralService.Remove(generalService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneralServiceExists(int id)
        {
          return (_context.GeneralService?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
