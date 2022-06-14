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
    public class ClientsServiceController : Controller
    {
        private readonly DBContext _context;

        public ClientsServiceController(DBContext context)
        {
            _context = context;
        }

        // GET: ClientsService
        public async Task<IActionResult> Index()
        {
            var DBContext = _context.ClientsServices.Include(c => c._Client);
            return View(await DBContext.ToListAsync());
        }

        // GET: ClientsService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClientsServices == null)
            {
                return NotFound();
            }

            var clientsService = await _context.ClientsServices
                .Include(c => c._Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientsService == null)
            {
                return NotFound();
            }

            return View(clientsService);
        }

        // GET: ClientsService/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id");
            return View();
        }

        // POST: ClientsService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,StaffId,ServiceDate")] ClientsService clientsService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientsService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", clientsService.ClientId);
            return View(clientsService);
        }

        // GET: ClientsService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClientsServices == null)
            {
                return NotFound();
            }

            var clientsService = await _context.ClientsServices.FindAsync(id);
            if (clientsService == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", clientsService.ClientId);
            return View(clientsService);
        }

        // POST: ClientsService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,StaffId,ServiceDate")] ClientsService clientsService)
        {
            if (id != clientsService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientsService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsServiceExists(clientsService.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", clientsService.ClientId);
            return View(clientsService);
        }

        // GET: ClientsService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClientsServices == null)
            {
                return NotFound();
            }

            var clientsService = await _context.ClientsServices
                .Include(c => c._Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientsService == null)
            {
                return NotFound();
            }

            return View(clientsService);
        }

        // POST: ClientsService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClientsServices == null)
            {
                return Problem("Entity set 'DBContext.ClientsServices'  is null.");
            }
            var clientsService = await _context.ClientsServices.FindAsync(id);
            if (clientsService != null)
            {
                _context.ClientsServices.Remove(clientsService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientsServiceExists(int id)
        {
          return (_context.ClientsServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
