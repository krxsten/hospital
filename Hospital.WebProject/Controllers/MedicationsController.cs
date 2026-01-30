using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Doctor;
using Hospital.WebProject.ViewModels.Medication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebProject.Controllers
{
    public class MedicationsController : Controller
    {
        private readonly HospitalDbContext Context;
        public MedicationsController(HospitalDbContext context)
        {
            this.Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var medications = await Context.Medications.Include(x => x.Diagnose).Select(x => new MedicationIndexViewModel
            {
                Name = x.Name,
                DiagnoseID = x.DiagnoseID,
                Diagnose = x.Diagnose,
                Description = x.Description
            }).ToListAsync();
            return View(medications);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Diagnose = new SelectList(Context.Diagnoses, "ID", "Name");
            return View(new MedicationCreateViewModel ());
        }
        [HttpPost]
        public async Task<IActionResult> Create(MedicationCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var medication = new Medication()
            {
                Name = model.Name,
                DiagnoseID= model.DiagnoseID,
                Diagnose= model.Diagnose,
                Description = model.Description,
                SideEffects = model.SideEffects
            };
            await Context.Medications.AddAsync(medication);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewBag.Diagnose = new SelectList(Context.Diagnoses, "ID", "Name");
            var medication = await Context. Medications.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }
            var model = new MedicationCreateViewModel
            {
                Name = medication.Name,
                DiagnoseID = medication.DiagnoseID,
                Diagnose = medication.Diagnose,
                Description = medication.Description,
                SideEffects = medication.SideEffects
            };
            return View(medication);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MedicationCreateViewModel model)
        {
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
