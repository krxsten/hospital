using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Diagnose;
using Hospital.WebProject.ViewModels.Medication;
using Hospital.WebProject.ViewModels.Nurse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class NursesController : Controller
    {
        private readonly HospitalDbContext Context;
        private readonly UserManager<User> UserManager;
        public NursesController(HospitalDbContext context, UserManager<User> userManager)
        {
            this.Context = context;
            this.UserManager = userManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                var docs = await Context.Nurses.Include(x => x.Shift).Include(x => x.Specialization).Include(x => x.User).Select(x => new NurseViewModel
                {
                    //SpecializationID = x.SpecializationId,
                    //Specialization = x.Specialization,
                    //ShiftID = x.ShiftId,
                    //Shift = x.Shift,
                    //UserID = x.UserId,
                    //User = x.User,
                    //IsAccepted = x.IsAccepted
                }).ToListAsync();
                return View(docs);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View(new NurseViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(NurseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var nurse = new Nurse()
            {
                //SpecializationId = model.SpecializationId,
                //Specialization = model.Specialization,
                //ShiftId = model.ShiftId,
                //Shift = model.Shift,
                //UserId = model.UserId,
                //User = model.User,
                //IsAccepted = model.IsAccepted
            };
            await Context.Nurses.AddAsync(nurse);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewBag.Shift = new SelectList(Context.Shifts, "ID", "Type");
            ViewBag.Specialization = new SelectList(Context.Specializations, "ID", "SpecializationName");
            var medication = await Context.Medications.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }
            var model = new MedicationViewModel
            {
                Name = medication.Name,
                DiagnoseID = medication.DiagnoseID,
                //Diagnose = medication.Diagnose,
                Description = medication.Description,
                SideEffects = medication.SideEffects
            };
            return View(medication);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MedicationViewModel model)
        {
            ViewBag.Shift = new SelectList(Context.Shifts, "ID", "Type");
            ViewBag.Specialization = new SelectList(Context.Specializations, "ID", "SpecializationName");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var medication = await Context.Medications.FindAsync(model.ID);
            if (medication == null)
            {
                return NotFound();
            }
            Context.Medications.Update(medication);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var medication = await Context.Medications.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }
            Context.Medications.Remove(medication);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
