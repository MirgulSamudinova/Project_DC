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
    public class PatientsController : Controller
    {
        private readonly DBContext _context;

        public PatientsController(DBContext context)
        {
            _context = context;

           
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {   
            var patients = _context.Patients
              .Include(c => c.Genders)
              .OrderBy(c => c.LastName)
              .AsNoTracking();
            return View(await patients.ToListAsync());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patients = await _context.Patients
                .Include(g=>g.Genders)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patients == null)
            {
                return NotFound();
            }

            return View(patients);
        }
        private void PopulationGendersDDL(object selectedGender = null)
        {
            var GendersQuery = from s in _context.Genders
                               orderby s.gender
                               select s;
            ViewBag.GenderID = new SelectList(GendersQuery.AsNoTracking(), "id_gender", "gender", selectedGender);

        }
        // GET: Patients/Create
        public IActionResult Create()
        {
            PopulationGendersDDL();
            return View();
        }


        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,LastName,FirstName,MiddleName,BirthDate,GenderId,MobileNumber,e_mail,inn")] Patients patients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulationGendersDDL(patients.GenderId);
            return View(patients);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patients = await _context.Patients
                .Include(g => g.Genders)
                .AsNoTracking()
                
                .FirstOrDefaultAsync(p=>p.PatientId == id);
            if(patients==null)
            {
                return NotFound();
            }

            PopulationGendersDDL(patients.GenderId);
            return View(patients);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           var PatientsToUpdate =  await _context.Patients
                .FirstOrDefaultAsync(p=>p.PatientId==id);

            if (await TryUpdateModelAsync<Patients>(PatientsToUpdate,
                "",
                c=>c.LastName,c=>c.FirstName,c=>c.MiddleName,c=>c.BirthDate,c=>c.GenderId, c=>c.MobileNumber,c=>c.e_mail,c=>c.inn ))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("dd", "ddd");
                }
                return RedirectToAction(nameof(Index));
            }

            PopulationGendersDDL(PatientsToUpdate.GenderId);
            return View(PatientsToUpdate);
           
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patients = await _context.Patients
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patients == null)
            {
                return NotFound();
            }

            return View(patients);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patients == null)
            {
                return Problem("Entity set 'DBContext.Patients'  is null.");
            }
            var patients = await _context.Patients.FindAsync(id);
            if (patients != null)
            {
                _context.Patients.Remove(patients);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientsExists(int id)
        {
          return (_context.Patients?.Any(e => e.PatientId == id)).GetValueOrDefault();
        }
    }
}
