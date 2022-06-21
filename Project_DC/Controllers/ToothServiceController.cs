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
    public class ToothServiceController : Controller
    {
        private readonly DBContext _context;

        public ToothServiceController(DBContext context)
        {
            _context = context;
        }

        // GET: ToothService
        public async Task<IActionResult> Index()
        {
            var DBContext = _context.ToothServices
                            .Include(t => t._DentalService)
                            .Include(t => t._ClientsTooth)
                                .ThenInclude(x=>x._Tooth)
                                .ThenInclude(x=>x._ToothSector);
            return View(await DBContext.ToListAsync());
        }

        // GET: ToothService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToothServices == null)
            {
                return NotFound();
            }

            var toothService = await _context.ToothServices
                .Include(t => t._DentalService)
                .Include(t => t._ClientsTooth).ThenInclude(x => x._Tooth)
                                              .ThenInclude(x => x._ToothSector)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toothService == null)
            {
                return NotFound();
            }

            return PartialView("ToothServiceDetails", toothService);
        }

        // GET: ToothService/Create
        public IActionResult Create(int? clientsServiceId)
        {
            _context.Teeth.Include(x => x._ToothSector).Load();
            var client = _context
                .ClientsServices
                .Include(x=>x._Patients)
                    .ThenInclude(x=>x.ClientsTeeth)
                .FirstOrDefault(x => x.Id == clientsServiceId);

            var dentalServicesList = _context.DentalServices.Where(x => x.IsToothService);
            var clientsTeeth = client._Patients.ClientsTeeth
                .Where(x=>x.PatientsId == client.PatientsId)
                .OrderBy(x=> x.ToothId);
            ViewData["DentalServiceId"] = new SelectList(dentalServicesList, "Id", "NameOfService");
            ViewData["ClientsToothId"] = new SelectList(clientsTeeth, "Id", "ToothDetails");
            return PartialView("ToothServiceModal");
        }

        // POST: ToothService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientsToothId,Id,DentalServiceId,Comment,Price")] ToothService toothService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toothService);
                await _context.SaveChangesAsync();
                if (toothService.IsToothStateChange)
                {
                    var clientsTooth = await _context.ClientsTeeth.FirstAsync(id => id.Id == toothService.ClientsToothId);
                    if(clientsTooth != null)
                    {
                        var dentalService = await _context.DentalServices.FirstAsync(id => id.Id == toothService.DentalServiceId);
                        if(dentalService != null && dentalService.ToothStateId != null)
                        {
                            clientsTooth.ToothStateId = (int)dentalService.ToothStateId;
                            _context.Update(clientsTooth);
                            await _context.SaveChangesAsync();
                        }
                        
                    }
                }
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["DentalServiceId"] = new SelectList(_context.DentalServices, "Id", "NameOfService", toothService.DentalServiceId);
            ViewData["ClientsToothId"] = new SelectList(_context.ClientsTeeth, "Id", "ToothDetails", toothService.ClientsToothId);
            return View(toothService);
        }

        // GET: ToothService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ToothServices == null)
            {
                return NotFound();
            }

            var toothService = await _context.ToothServices
                .Include(t => t._DentalService)
                .Include(t => t._ClientsTooth).ThenInclude(x => x._Tooth).ThenInclude(x => x._ToothSector)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (toothService == null)
            {
                return NotFound();
            }
            ViewData["DentalServiceId"] = new SelectList(_context.DentalServices, "Id", "NameOfService", toothService.DentalServiceId);
            ViewData["ClientsToothId"] = new SelectList(_context.ClientsTeeth, "Id", "ToothDetails", toothService.ClientsToothId);
            return PartialView("ToothServiceModal", toothService);
            //return View(toothService);
        }

        // POST: ToothService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientsToothId,Id,DentalServiceId,Comment,Price")] ToothService toothService)
        {
            if (id != toothService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toothService);
                    await _context.SaveChangesAsync();

                    if (toothService.IsToothStateChange)
                    {
                        var clientsTooth = await _context.ClientsTeeth.FirstAsync(id => id.Id == toothService.ClientsToothId);
                        if (clientsTooth != null)
                        {
                            var dentalService = await _context.DentalServices.FirstAsync(id => id.Id == toothService.DentalServiceId);
                            if (dentalService != null && dentalService.ToothStateId != null)
                            {
                                clientsTooth.ToothStateId = (int)dentalService.ToothStateId;
                                _context.Update(clientsTooth);
                                await _context.SaveChangesAsync();
                            }

                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToothServiceExists(toothService.Id))
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
            ViewData["DentalServiceId"] = new SelectList(_context.DentalServices, "Id", "NameOfService", toothService.DentalServiceId);
            ViewData["ClientsToothId"] = new SelectList(_context.ClientsTeeth, "Id", "ToothDetails", toothService.ClientsToothId);
            return View(toothService);
        }

        // GET: ToothService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ToothServices == null)
            {
                return NotFound();
            }

            var toothService = await _context.ToothServices
                .Include(t => t._DentalService)
                .Include(t => t._ClientsTooth)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toothService == null)
            {
                return NotFound();
            }

            return View(toothService);
        }

        // POST: ToothService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ToothServices == null)
            {
                return Problem("Entity set 'DBContext.ToothServices'  is null.");
            }
            var toothService = await _context.ToothServices.FindAsync(id);
            if (toothService != null)
            {
                _context.ToothServices.Remove(toothService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToothServiceExists(int id)
        {
          return (_context.ToothServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
