using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.ViewModels.Diagnose;
using Hospital.WebProject.ViewModels.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Hospital.WebProject.Controllers
{
    [Authorize]
    public class DiagnosesController : Controller
    {
        private readonly HospitalDbContext Context;
        private readonly UserManager<User> UserManager;
        public DiagnosesController(HospitalDbContext context, UserManager<User> userManager)
        {
            this.Context = context;
            this.UserManager = userManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var diagnoses = await Context.Diagnoses.Select(x => new DiagnoseViewModel
            {
                ID = x.ID,
                Name = x.Name,
                ListOfPatientsAndDiagnoses=x.ListOfPatientsAndDiagnoses,
                Image=x.Image
            }).ToListAsync();
            return View(diagnoses);

        }
        [Authorize(Roles = "Admin, Doctor")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new DiagnoseViewModel());
        }
        [Authorize(Roles = "Admin, Doctor")]
        [HttpPost]
        public async Task<IActionResult> Create(DiagnoseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var diagnose = new Diagnose()
            {
                ID = Guid.NewGuid(),
                Name = model.Name,
                ListOfPatientsAndDiagnoses = model.ListOfPatientsAndDiagnoses,
                ListOfMedication=model.ListOfMedication,
                Image = model.Image
            };
            await Context.Diagnoses.AddAsync(diagnose);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, Doctor")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var diagnose = await Context.Diagnoses.FindAsync(id);
            if (diagnose == null)
            {
                return NotFound();
            }
            var model = new DiagnoseViewModel
            {
                ID = diagnose.ID,
                Name = diagnose.Name,
                ListOfPatientsAndDiagnoses = diagnose.ListOfPatientsAndDiagnoses,
                Image = diagnose.Image
            };
            return View(diagnose);
        }
        [Authorize(Roles = "Admin, Doctor")]
        [HttpPost]
        public async Task<IActionResult> Edit(DiagnoseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var diagnose = await Context.Diagnoses.FindAsync(model.ID);
            if (diagnose == null)
            {
                return NotFound();
            }
            Context.Diagnoses.Update(diagnose);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, Doctor")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var diagnose = await Context.Diagnoses.FindAsync(id);
            if (diagnose == null)
            {
                return NotFound();
            }
            Context.Diagnoses.Remove(diagnose);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
