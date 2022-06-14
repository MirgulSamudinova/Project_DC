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
    public class StaffsController : Controller
    {
        private readonly DBContext _context;

        public StaffsController(DBContext context)
        {
            
            _context = context;
        }

        // GET: Staffs
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.Staffs.Include(s => s.Genders).Include(s => s.Positions);
            
            return View(await dBContext.ToListAsync());
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs
                .Include(s => s.Genders)
                .Include(s => s.Positions)
                .FirstOrDefaultAsync(m => m.id_staff == id);
            if (staffs == null)
            {
                return NotFound();
            }

            return View(staffs);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            //ViewData["id_gender"] = new SelectList(_context.Set<Genders>(), "id_gender", "id_gender");
           // ViewData["id_position"] = new SelectList(_context.Positions, "Id_position", "Id_position");
            PopulateGenderDropDownList();
            PopulatePositionDropDownList();
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_staff,sure_name,first_name,middle_name,id_gender,id_position,birth_date,mobile_number,e_mail")] Staffs staffs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["id_gender"] = new SelectList(_context.Set<Genders>(), "id_gender", "id_gender", staffs.id_gender);
            //ViewData["id_position"] = new SelectList(_context.Positions, "Id_position", "Id_position", staffs.id_position);
            PopulateGenderDropDownList(staffs.id_gender);
            PopulatePositionDropDownList(staffs.id_position);
            return View(staffs);
        }


        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Staffs == null)
        //    {
        //        return NotFound();
        //    }

        //    var staffs = await _context.Staffs.FindAsync(id);
        //    if (staffs == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["id_gender"] = new SelectList(_context.Set<Genders>(), "id_gender", "id_gender", staffs.id_gender);
        //    ViewData["id_position"] = new SelectList(_context.Positions, "Id_position", "Id_position", staffs.id_position);
        //    return View(staffs);
        //}

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(id== null)

            {
                return NotFound();
            }
            var staffs =await _context.Staffs
                .AsNoTracking()
                .FirstOrDefaultAsync(s=>s.id_staff==id);
            if (staffs==null)
            {
                return NotFound();
            }
            PopulateGenderDropDownList(staffs.id_gender);
            PopulatePositionDropDownList(staffs.id_position);

            return View(staffs);
        }

        private void PopulatePositionDropDownList(object selectedPosition = null)
        {
            var PositionsQuery = from s in _context.Positions
                               orderby s.position
                               select s;
            ViewBag.id_position = new SelectList(PositionsQuery.AsNoTracking(), "Id_position", "position", selectedPosition);
        }

        private void PopulateGenderDropDownList(object  selectedGender=null)
        {
            var GendersQuery= from s in _context.Genders
                                  orderby s.gender
                              select s;
            ViewBag.id_gender = new SelectList(GendersQuery.AsNoTracking(),"id_gender","gender", selectedGender);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        #region oldCode

        //public async Task<IActionResult> Edit(int id, [Bind("id_staff,sure_name,first_name,middle_name,id_gender,id_position,birth_date,mobile_number,e_mail")] Staffs staffs)
        //{
        //    if (id != staffs.id_staff)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(staffs);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!StaffsExists(staffs.id_staff))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //        PopulateGenderDropDownList(staffs.id_gender);
        //        PopulatePositionDropDownList(staffs.id_position);
        //        return View(staffs);
        //    }
        //}
        #endregion
        [HttpPost, ActionName("Edit")]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var staffToUpdate = await _context.Staffs
                .FirstOrDefaultAsync(s => s.id_staff == id);
          

                    if (await TryUpdateModelAsync<Staffs>(staffToUpdate,
                    "",
                    s => s.id_staff, s => s.sure_name, s => s.first_name, s => s.middle_name, s => s.id_gender, s => s.id_position, s => s.birth_date, s => s.e_mail, s => s.mobile_number))
                    {
                        //try
                        //{
                        await _context.SaveChangesAsync();

                        //}
                        //catch (DbUpdateException )
                        //{
                        //    ModelState.AddModelError("", "Unable to save changes" +
                        //        "try again? and if the problem persits, " +
                        //        "see your system admin");

                        //}
                        //



                        return RedirectToAction(nameof(Index));
                    }

                    PopulateGenderDropDownList(staffToUpdate.id_gender);
                    PopulatePositionDropDownList(staffToUpdate.id_position);
                    return View(staffToUpdate);
                }
                // GET: Staffs/Delete/5



                public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs
                .Include(s => s.Genders)
                .Include(s => s.Positions)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.id_staff == id);
            if (staffs == null)
            {
                return NotFound();
            }

            return View(staffs);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Staffs == null)
            {
                return Problem("Entity set 'DBContext.Staffs'  is null.");
            }
            var staffs = await _context.Staffs.FindAsync(id);
            if (staffs != null)
            {
                _context.Staffs.Remove(staffs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffsExists(int id)
        {
          return (_context.Staffs?.Any(e => e.id_staff == id)).GetValueOrDefault();
        }
    }
}
