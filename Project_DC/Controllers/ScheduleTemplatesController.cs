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
    public class ScheduleTemplatesController : Controller
    {
        private readonly DBContext _context;

        public ScheduleTemplatesController(DBContext context)
        {
            _context = context;
        }

        // GET: ScheduleTemplates
        public async Task<IActionResult> Index()

        {
              var temp =_context.ScheduleTemplate
                .Include(s=>s.Staffs)
                 .Include(q => q.DaysOfWeek)
                .AsNoTracking();
            return View(await temp.ToListAsync());
        }
        private void PopulateDaysDropDownList(object selectedDayOfWeek = null)
        {
            var DayOfWeekQuery = from d in _context.DaysOfWeek
                                 orderby d.DaysOfWeekID
                                 select d;
            ViewBag.DayOfWeek = new SelectList(DayOfWeekQuery.AsNoTracking(), "DaysOfWeekID", "DaysOfWeekName", selectedDayOfWeek);
        }

        private void PopulateStaffsDropDownList(object selectedStaffs = null)
        {
            var staffsQuery = from d in _context.Staffs
                                   orderby d.first_name
                                   select d;
            ViewBag.StaffId = new SelectList(staffsQuery.AsNoTracking(), "id_staff", "FullName", selectedStaffs);
        }
       
        // GET: ScheduleTemplates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ScheduleTemplate == null)
            {
                return NotFound();
            }
            var ScheduleTemplate = await _context.ScheduleTemplate
          .Include(c => c.Staffs)
          .Include(d=>d.DaysOfWeek)
           .AsNoTracking()
           .FirstOrDefaultAsync(m => m.TmeplateId == id);
            if (ScheduleTemplate == null)
            {
                return NotFound();
            }

            return View(ScheduleTemplate);
        }

        // GET: ScheduleTemplates/Create
        public IActionResult Create()
        {
            PopulateStaffsDropDownList();
            PopulateDaysDropDownList();
            return View();
        }

        // POST: ScheduleTemplates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TmeplateId,StaffId,DayOfWeek,withTime,ToTime")] ScheduleTemplate scheduleTemplate)
        {

            if (ModelState.IsValid)
            {
                _context.Add(scheduleTemplate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateStaffsDropDownList(scheduleTemplate.StaffId);
            PopulateDaysDropDownList(scheduleTemplate.DayOfWeek);
            return View(scheduleTemplate);
        }

        // GET: ScheduleTemplates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var scheduleTemplate = await _context.ScheduleTemplate
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.TmeplateId == id);

            if (scheduleTemplate == null)
            {
                return NotFound();
            }
            PopulateStaffsDropDownList(scheduleTemplate.StaffId);
            PopulateDaysDropDownList(scheduleTemplate.DayOfWeek);
            return View(scheduleTemplate);
        }

        // POST: ScheduleTemplates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TmeplateId,StaffId,DayOfWeek,withTime,ToTime")] ScheduleTemplate scheduleTemplate)
        {
            if (id != scheduleTemplate.TmeplateId)
            {
                return NotFound();
            }
            var scheduleTempale = await _context.ScheduleTemplate
                .FirstOrDefaultAsync(s => s.TmeplateId == id);
            if(await TryUpdateModelAsync<ScheduleTemplate>(scheduleTempale,
                    "",
                     s=>s.Staffs,d=>d.DaysOfWeek))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Невозможно сохранить");
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateStaffsDropDownList(scheduleTempale.StaffId);
            PopulateDaysDropDownList(scheduleTempale.DayOfWeek);
            return View(scheduleTemplate);
        }

        // GET: ScheduleTemplates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ScheduleTemplate == null)
            {
                return NotFound();
            }

            var scheduleTemplate = await _context.ScheduleTemplate
                .Include(s=>s.Staffs)
                .Include(d => d.DaysOfWeek)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.TmeplateId == id);
            if (scheduleTemplate == null)
            {
                return NotFound();
            }
            PopulateStaffsDropDownList(scheduleTemplate.StaffId);
            PopulateDaysDropDownList(scheduleTemplate.DayOfWeek);
            return View(scheduleTemplate);
        }

        // POST: ScheduleTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ScheduleTemplate == null)
            {
                return Problem("Entity set 'DBContext.ScheduleTemplate'  is null.");
            }
            var scheduleTemplate = await _context.ScheduleTemplate.FindAsync(id);
            if (scheduleTemplate != null)
            {
                _context.ScheduleTemplate.Remove(scheduleTemplate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleTemplateExists(int id)
        {
          return (_context.ScheduleTemplate?.Any(e => e.TmeplateId == id)).GetValueOrDefault();
        }
    }
}
